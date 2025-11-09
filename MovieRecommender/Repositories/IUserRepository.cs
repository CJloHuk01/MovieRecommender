using MovieRecommender.Models;

namespace MovieRecommender.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User? GetUserByEmail(string email);
    }
}

