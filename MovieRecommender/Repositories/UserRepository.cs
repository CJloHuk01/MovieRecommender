using Microsoft.EntityFrameworkCore;
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
        public User AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }
        public bool DeleteUser(int id)
        {
            var user = _context.Users.FirstOrDefault(u =>
            u.Id == id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
                return true;
            }
            else return false;

        }

        public User ExistUser(string loginOrEmail)
        {
            var user = _context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u =>
            u.Username == loginOrEmail ||
            u.Email == loginOrEmail);

            return user;
        }

        public IEnumerable<User> GetAll() => _context.Users.ToList();

        public User? GetById(int id) => _context.Users.FirstOrDefault(x => x.Id == id);

        public Role? RoleExist(int id)
        {
            return _context.Roles.FirstOrDefault(u => u.Id == id);
        }
        public User UpdateUser(int id, User updatedUser)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.Id == id);
            if (existingUser != null)
            {
                existingUser.Username = updatedUser.Username;
                existingUser.Email = updatedUser.Email;
                existingUser.UpdateAt = DateTime.UtcNow;

                _context.SaveChanges();
                return existingUser;
            }
            return null;
        }


    }
}
