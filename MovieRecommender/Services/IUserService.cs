using MovieRecommender.Models.DTO;

namespace MovieRecommender.Services
{
    public interface IUserService
    {
        AuthResponse Login(LoginRequest loginRequest);
        AuthResponse Register(CreateUserRequest createUserRequest);
        AuthResponse RefreshToken(string refreshToken);
        bool ValidateToken(string token);
        object GetUserById(int id);
    }
}
