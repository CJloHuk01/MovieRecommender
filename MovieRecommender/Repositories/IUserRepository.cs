using MovieRecommender.Models;

namespace MovieRecommender.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUserById(int id);
        User AddUser(User user);
        bool DeleteUser(int id);
        User UpdateUser(int id, User user);
        User ExistUser(string loginOrEmail);
        Role? RoleExist(int id);
    }
}

