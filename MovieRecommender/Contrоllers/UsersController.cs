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

        //[HttpGet]
        //public ActionResult<IEnumerable<User>> Get()
        //{
        //    return Ok(_userRepository.GetAll());
        //}

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
                return NotFound(new { message = $"Пользователь с ID {id} не найден" });

            return Ok(user);
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {
            var createdUser = _userRepository.AddUser(user);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User user)
        {
            var updatedUser = _userRepository.UpdateUser(id, user);
            if (updatedUser == null)
                return NotFound(new { message = $"Пользователь с ID {id} не найден" });

            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var result = _userRepository.DeleteUser(id);
            if (!result)
                return NotFound(new { message = $"Пользователь с ID {id} не найден" });

            return NoContent();
        }
    }
}
    

