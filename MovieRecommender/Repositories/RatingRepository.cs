using Microsoft.EntityFrameworkCore;
using MovieRecommender.Models;

namespace MovieRecommender.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly ApplicationDbContext _context;

        public RatingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Rating? GetById(int id)
        {
            return _context.Ratings
                .Include(r => r.User) 
                .Include(r => r.Movie) 
                .ThenInclude(m => m.Genre) 
                .FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Rating> GetAll()
        {
            return _context.Ratings
                .Include(r => r.User)
                .Include(r => r.Movie)
                .ThenInclude(m => m.Genre)
                .ToList();
        }

        public Rating Create(Rating entity)
        {
            _context.Ratings.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public Rating Update(Rating entity)
        {
            _context.Ratings.Update(entity);
            _context.SaveChanges();

            return _context.Ratings
                .Include(r => r.User)
                .Include(r => r.Movie)
                .FirstOrDefault(r => r.Id == entity.Id) ?? entity;
        }

        public bool Delete(int id)
        {
            var rating = GetById(id);
            if (rating == null) return false;

            _context.Ratings.Remove(rating);
            _context.SaveChanges();
            return true;
        }

        public bool Exists(int id) => _context.Ratings.Any(r => r.Id == id);

        public IEnumerable<Rating> GetRatingsByUser(int userId)
        {
            return _context.Ratings
                .Where(r => r.UserId == userId)
                .Include(r => r.User)     
                .Include(r => r.Movie)    
                .ToList();
        }

        public IEnumerable<Rating> GetRatingsByMovie(int movieId)
        {
            return _context.Ratings
                .Where(r => r.MovieId == movieId)
                .Include(r => r.User)     
                .Include(r => r.Movie)    
                .ToList();
        }

        public bool HasUserRatedMovie(int userId, int movieId)
        {
            return _context.Ratings
                .Any(r => r.UserId == userId && r.MovieId == movieId);
        }

        public Rating? GetUserRatingForMovie(int userId, int movieId)
        {
            return _context.Ratings
                .FirstOrDefault(r => r.UserId == userId && r.MovieId == movieId);
        }   
    }
}
