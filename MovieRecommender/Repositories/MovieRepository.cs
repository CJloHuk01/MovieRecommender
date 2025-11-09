using MovieRecommender.Models;
using Microsoft.EntityFrameworkCore;

namespace MovieRecommender.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ApplicationDbContext _context;

        public MovieRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Movie> GetAll() => _context.Movies.ToList();

        public Movie? GetById(int id) => _context.Movies.Find(id);

        public Movie Create(Movie entity)
        {
            _context.Movies.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public Movie Update(Movie entity)
        {
            _context.Movies.Update(entity);
            _context.SaveChanges();
            return entity;
        }

        public bool Delete(int id)
        {
            var movie = GetById(id);
            if (movie == null) return false;

            _context.Movies.Remove(movie);
            _context.SaveChanges();
            return true;
        }

        public bool Exists(int id) => _context.Movies.Any(m => m.Id == id);

        public IEnumerable<Movie> GetMoviesByGenre(int genreId)
            => _context.Movies.Where(m => m.GenreId == genreId).ToList();

        public Movie? GetMovieWithDetails(int id)
            => _context.Movies
                .Include(m => m.Genre)
                .Include(m => m.Ratings)
                .FirstOrDefault(m => m.Id == id);
    }
}
