using UserApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserApi.Repositories.Interfaces
{
    public interface IUserRepository
    {

        Task<User> GetUserByEmail(string email);
        Task<bool> UserEmailExists(string email);
        Task<User> GetUserById(int id);
        Task AddUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(int id);
        Task<IEnumerable<User>> GetAllUsers();
    }
}