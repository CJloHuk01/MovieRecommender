using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieRecommender.Models.DTO;
using MovieRecommender.Services;

namespace MovieRecommender.Contrоllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public IActionResult GetAllGenres()
        {
            var genres = _genreService.GetAllGenres();
            return Ok(genres);
        }

        [HttpGet("{id}")]
        public IActionResult GetGenreById(int id)
        {
            var genre = _genreService.GetGenreById(id);
            if (genre == null)
                return NotFound(new { message = $"Жанр с ID {id} не найден" });

            return Ok(genre);
        }

        [HttpPost]
        public IActionResult CreateGenre([FromBody] CreateGenreDTO createGenreDto)
        {
            var genre = _genreService.CreateGenre(createGenreDto);
            return CreatedAtAction(nameof(GetGenreById), new { id = genre.Id }, genre);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGenre(int id)
        {
            var result = _genreService.DeleteGenre(id);
            if (!result)
                return NotFound(new { message = $"Жанр с ID {id} не найден" });

            return NoContent();
        }
    }
}
