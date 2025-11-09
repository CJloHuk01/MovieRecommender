using MovieRecommender.Models;

namespace MovieRecommender.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAll() => _context.Users.ToList();

        public User? GetById(int id) => _context.Users.Find(id);

        public User Create(User entity)
        {
            _context.Users.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public User Update(User entity)
        {
            _context.Users.Update(entity);
            _context.SaveChanges();
            return entity;
        }

        public bool Delete(int id)
        {
            var user = GetById(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            _context.SaveChanges();
            return true;
        }

        public bool Exists(int id) => _context.Users.Any(u => u.Id == id);

        public User? GetUserByEmail(string email)
            => _context.Users.FirstOrDefault(u => u.Email == email);
    }
}
