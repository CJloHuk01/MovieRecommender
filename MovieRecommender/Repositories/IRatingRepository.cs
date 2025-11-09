using MovieRecommender.Models;

namespace MovieRecommender.Repositories
{
    public interface IRatingRepository : IRepository<Rating>
    {
        IEnumerable<Rating> GetRatingsByUser(int userId);
        IEnumerable<Rating> GetRatingsByMovie(int movieId);
    }
}
