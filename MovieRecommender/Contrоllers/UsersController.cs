using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieRecommender.Models;
using MovieRecommender.Models.DTO;
using MovieRecommender.Repositories;
using MovieRecommender.Services;

namespace MovieRecommender.Contrоllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userRepository.GetAll();

            var userDtos = users.Select(user => new UserDTO
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                RoleName = user.Role.Name
            }).ToList();

            return Ok(userDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
                return NotFound(new { message = $"Пользователь с ID {id} не найден" });

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var result = _userRepository.DeleteUser(id);
            if (!result)
                return NotFound(new { message = $"Пользователь с ID {id} не найден" });

            return NoContent();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User user)
        {
            var updatedUser = _userRepository.UpdateUser(id, user);
            if (updatedUser == null)
                return NotFound(new { message = $"Пользователь с ID {id} не найден" });
            updatedUser.Username = user.Username;
            updatedUser.Email = user.Email;
            updatedUser.IsActive = user.IsActive;
            updatedUser.UpdateAt = DateTime.UtcNow;

            return Ok(updatedUser);
        }
    }
}
    

