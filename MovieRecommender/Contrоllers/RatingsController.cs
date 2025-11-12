using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieRecommender.Models.DTO;
using MovieRecommender.Services;

namespace MovieRecommender.Contrоllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly IRatingService _ratingService;

        public RatingsController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetUserRatings(int userId)
        {
            var ratings = _ratingService.GetUserRatings(userId);
            return Ok(ratings);
        }

        [HttpGet("movie/{movieId}")]
        public IActionResult GetMovieRatings(int movieId)
        {
            var ratings = _ratingService.GetMovieRatings(movieId);
            return Ok(ratings);
        }

        [HttpPost]
        public IActionResult AddRating([FromBody] CreateRatingDTO createRatingDto)
        {
            var rating = _ratingService.AddRating(createRatingDto);
            return Ok(rating);
        }

        [HttpGet("user/{userId}/movie/{movieId}")]
        public IActionResult HasUserRatedMovie(int userId, int movieId)
        {
            var hasRated = _ratingService.HasUserRatedMovie(userId, movieId);
            return Ok(new { hasRated });
        }
    }
}
