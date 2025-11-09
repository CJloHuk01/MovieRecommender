using Microsoft.EntityFrameworkCore;
using MovieRecommender.Models;

namespace MovieRecommender.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDbContext _context;

        public GenreRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Genre> GetAll() => _context.Genres.ToList();

        public Genre? GetById(int id) => _context.Genres.Find(id);

        public Genre Create(Genre entity)
        {
            _context.Genres.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public Genre Update(Genre entity)
        {
            _context.Genres.Update(entity);
            _context.SaveChanges();
            return entity;
        }

        public bool Delete(int id)
        {
            var genre = GetById(id);
            if (genre == null) return false;

            _context.Genres.Remove(genre);
            _context.SaveChanges();
            return true;
        }

        public bool Exists(int id) => _context.Genres.Any(g => g.Id == id);

        public Genre? GetGenreWithMovies(int id)
            => _context.Genres
                .Include(g => g.Movies)
                .FirstOrDefault(g => g.Id == id);
    }
}
