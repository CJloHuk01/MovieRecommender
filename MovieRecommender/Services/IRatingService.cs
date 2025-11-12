using MovieRecommender.Models.DTO;

namespace MovieRecommender.Services
{
    public interface IRatingService
    {
        RatingDTO AddRating(CreateRatingDTO createRatingDto);
        IEnumerable<RatingDTO> GetUserRatings(int userId);
        IEnumerable<RatingDTO> GetMovieRatings(int movieId);
        bool HasUserRatedMovie(int userId, int movieId);
    }
}
