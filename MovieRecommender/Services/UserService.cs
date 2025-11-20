using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MovieRecommender.Models;
using MovieRecommender.Models.DTO;
using MovieRecommender.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MovieRecommender.Services
{
    public class UserService : IUserService
    {
        private readonly JwtConfiguration _jwtSett;
        private readonly IUserRepository _userRepository;

        public UserService(IOptions<JwtConfiguration> jwtConf, IUserRepository userRepo)
        {
            _jwtSett = jwtConf.Value;
            _userRepository = userRepo;
        }
        /// <summary>
        /// Аутентификация пользователя
        /// </summary>
        public AuthResponse Login(LoginRequest loginRequest)
        {
            try
            {
                var user = _userRepository.ExistUser(loginRequest.LoginOrEmail);
                

                if (user == null)
                    return new AuthResponse
                    {
                        Success = false,
                        ErrorMessage = "Такого пользователя не существует"
                    };

                if (user.PasswordHash != GetHashPassword(loginRequest.Password))
                    return new AuthResponse
                    {
                        Success = false,
                        ErrorMessage = "Пароль не соответствует"
                    };
                else
                {
                    var token = GenerateJwtToken(user);
                    var refreshToken = GenerateRefreshToken();

                    return new AuthResponse
                    {
                        Success = true,
                        Token = token,
                        RefreshToken = refreshToken,
                        ValidTo = DateTime.UtcNow.AddMinutes(_jwtSett.ExpirateAtInMinutes),
                        User = new UserDTO
                        {
                            Id = user.Id,
                            Username = user.Username,
                            Email = user.Email,
                            RoleName = user.Role.Name
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                return new AuthResponse
                {
                    Success = false,
                    ErrorMessage = $"Некорректные данные: {ex.Message}"
                };
            }
        }
        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        public AuthResponse Register(CreateUserRequest createUserRequest)
        {
            try
            {
                var role = _userRepository.RoleExist(createUserRequest.RoleId);
                if (role == null)
                    throw new ArgumentException("Роль с таким id не существует");

                var user1 = _userRepository.ExistUser(createUserRequest.Username);
                var user2 = _userRepository.ExistUser(createUserRequest.Email);

                if (user1 != null || user2 != null)
                {
                    return new AuthResponse
                    {
                        Success = false,
                        ErrorMessage = "Такой пользователь уже существует"
                    };
                }
                else
                {
                    User newUser = new User
                    {
                        Username = createUserRequest.Username,
                        Email = createUserRequest.Email,
                        PasswordHash = GetHashPassword(createUserRequest.Password),
                        CreatedAt = DateTime.UtcNow,
                        IsActive = true,
                        RoleId = createUserRequest.RoleId
                    };

                    var addedUser = _userRepository.AddUser(newUser);

                    return new AuthResponse
                    {
                        Success = true,
                        Token = GenerateJwtToken(addedUser),
                        RefreshToken = GenerateRefreshToken(),
                        ValidTo = DateTime.UtcNow.AddMinutes(_jwtSett.ExpirateAtInMinutes),
                        User = new UserDTO
                        {
                            Id = addedUser.Id,
                            Username = addedUser.Username,
                            Email = addedUser.Email,
                            RoleName = addedUser.Role.Name
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                return new AuthResponse
                {
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public AuthResponse RefreshToken(string refreshToken)
        {
            // TODO: Implement refresh token logic
            throw new NotImplementedException();
        }

        public bool ValidateToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtSett.SecretKey);

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _jwtSett.Issuer,
                    ValidateAudience = true,
                    ValidAudience = _jwtSett.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return validatedToken != null;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSett.SecretKey);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role?.Name ?? "User")
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSett.ExpirateAtInMinutes),
                Issuer = _jwtSett.Issuer,
                Audience = _jwtSett.Audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rnd = RandomNumberGenerator.Create();
            rnd.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private string GetHashPassword(string password)
        {
            byte[] bytepass = Encoding.ASCII.GetBytes(password);
            var hashBytes = SHA256.HashData(bytepass);
            return Convert.ToBase64String(hashBytes);
        }

    }
}