using MovieRecommender.Models;

namespace MovieRecommender.Repositories
{
    public interface IRatingRepository : IRepository<Rating>
    {
        IEnumerable<Rating> GetRatingsByUser(int userId);
        IEnumerable<Rating> GetRatingsByMovie(int movieId);
        bool HasUserRatedMovie(int userId, int movieId); 
        Rating? GetUserRatingForMovie(int userId, int movieId);
    }
}
