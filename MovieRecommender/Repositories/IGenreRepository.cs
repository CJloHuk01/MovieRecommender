using MovieRecommender.Models;

namespace MovieRecommender.Repositories
{
    public interface IGenreRepository : IRepository<Genre>
    {
        Genre? GetGenreWithMovies(int id);
    }
}
