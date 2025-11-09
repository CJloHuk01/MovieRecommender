using MovieRecommender.Models;

namespace MovieRecommender.Repositories
{
    public interface IMovieRepository : IRepository<Movie>
    {
        IEnumerable<Movie> GetMoviesByGenre(int genreId);
        Movie? GetMovieWithDetails(int id);
    }
}
