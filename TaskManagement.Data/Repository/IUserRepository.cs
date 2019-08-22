using System.Threading.Tasks;
using TaskManagement.Model;

namespace TaskManagement.Data.Repository
{
    public interface IUserRepository:IRepository<User>
    {
        Task<User> Register(User user, string password);

        Task<User> Login(string username, string password);

        Task<bool> UserExists(string email);

    }
}
