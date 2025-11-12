using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieRecommender.Models;
using MovieRecommender.Models.DTO;
using MovieRecommender.Services;

namespace MovieRecommender.Contrоllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
                return NotFound(new { message = $"Пользователь с ID {id} не найден" });

            return Ok(user);
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] CreateUserRequest createUserRequest)
        {
            var result = _userService.Register(createUserRequest);
            if (!result.Success)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(result);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            var result = _userService.Login(loginRequest);
            if (!result.Success)
                return Unauthorized(new { message = result.ErrorMessage });

            return Ok(result);
        }

        [HttpPost("validate-token")]
        public IActionResult ValidateToken([FromBody] string token)
        {
            var isValid = _userService.ValidateToken(token);
            return Ok(new { valid = isValid });
        }

        //[HttpDelete("{id}")]
        //public IActionResult DeleteUser(int id)
        //{
        //    var result = _userService.DeleteUser(id);
        //    if (!result)
        //        return NotFound(new { message = $"Пользователь с ID {id} не найден" });

        //    return NoContent();
        //}
    }
}
