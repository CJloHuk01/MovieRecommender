using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieRecommender.Models.DTO;
using MovieRecommender.Services;

namespace MovieRecommender.Contrоllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        [HttpPost("register")]
        public IActionResult Register([FromBody] CreateUserRequest createUserRequest)
        {
            var result = _userService.Register(createUserRequest);
            if (!result.Success)
                return BadRequest(new { error = result.ErrorMessage });

            return Ok(new { userId = result.User.Id, message = "User registered successfully" });
        }

        /// <summary>
        /// Вход в систему
        /// </summary>
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            var result = _userService.Login(loginRequest);
            if (!result.Success)
                return Unauthorized(new { error = result.ErrorMessage });

            return Ok(result);
        }

        [HttpPost("validate-token")]
        public IActionResult ValidateToken([FromBody] string token = "") 
        {
            if (string.IsNullOrEmpty(token))
                return BadRequest(new { error = "Token is required" });

            var isValid = _userService.ValidateToken(token);
            return Ok(new { valid = isValid });
        }

    }
}
