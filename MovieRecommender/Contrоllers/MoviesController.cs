using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieRecommender.Models.DTO;
using MovieRecommender.Services;

namespace MovieRecommender.Contrоllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAllMovies()
        {
            var movies = _movieService.GetAllMovies();
            return Ok(movies);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult GetMovieById(int id)
        {
            var movie = _movieService.GetMovieById(id);
            if (movie == null)
                return NotFound(new { message = $"Фильм с ID {id} не найден" });

            return Ok(movie);
        }

        [HttpGet("genre/{genreId}")]
        [AllowAnonymous]
        public IActionResult GetMoviesByGenre(int genreId)
        {
            var movies = _movieService.GetMoviesByGenre(genreId);
            return Ok(movies);
        }

        [HttpGet("top-rated")]
        [AllowAnonymous]
        public IActionResult GetTopRatedMovies([FromQuery] int count = 10)
        {
            var movies = _movieService.GetTopRatedMovies(count);
            return Ok(movies);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateMovie([FromBody] CreateMovieDTO createMovieDto)
        {
            var movie = _movieService.CreateMovie(createMovieDto);
            return CreatedAtAction(nameof(GetMovieById), new { id = movie.Id }, movie);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateMovie(int id, [FromBody] UpdateMovieDTO updateMovieDto)
        {
            var movie = _movieService.UpdateMovie(id, updateMovieDto);
            if (movie == null)
                return NotFound(new { message = $"Фильм с ID {id} не найден" });

            return Ok(movie);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteMovie(int id)
        {
            var result = _movieService.DeleteMovie(id);
            if (!result)
                return NotFound(new { message = $"Фильм с ID {id} не найден" });

            return NoContent();
        }
    }
}
