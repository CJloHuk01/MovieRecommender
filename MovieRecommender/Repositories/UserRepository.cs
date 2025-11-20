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


        public IEnumerable<User> GetAll()
        {
            return _context.Users.Include(u => u.Role).Include(u => u.Ratings).ToList();
        }

        public User? GetById(int id)
        {
            return _context.Users
                .Include(u => u.Role)
                .Include(u => u.Ratings)
                .FirstOrDefault(u => u.Id == id);
        }

        public User Create(User entity)
        {
            return AddUser(entity);
        }

        public User Update(User entity)
        {
            _context.Users.Update(entity);
            _context.SaveChanges();
            return entity;
        }

        public bool Delete(int id)
        {
            return DeleteUser(id);
        }

        public bool Exists(int id)
        {
            return _context.Users.Any(u => u.Id == id);
        }

        public User AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public bool DeleteUser(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
                return true;
            }
            else return false;
        }

        public User UpdateUser(int id, User user)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.Id == id);
            if (existingUser != null)
            {
                _context.Users.Update(user);
                _context.SaveChanges();
            }
            return user;
        }

        public User ExistUser(string loginOrEmail)
        {
            var user = _context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Username == loginOrEmail || u.Email == loginOrEmail);

            return user;
        }

        public Role? RoleExist(int id)
        {
            return _context.Roles.FirstOrDefault(r => r.Id == id);
        }
    }
}
