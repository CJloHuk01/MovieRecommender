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

        public IEnumerable<Rating> GetAll() => _context.Ratings.ToList();

        public Rating? GetById(int id) => _context.Ratings.Find(id);

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
            return entity;
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
            => _context.Ratings.Where(r => r.UserId == userId).ToList();

        public IEnumerable<Rating> GetRatingsByMovie(int movieId)
            => _context.Ratings.Where(r => r.MovieId == movieId).ToList();
    }
}
