using UserApi.DTOs;
using UserApi.Models;

namespace UserApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(int id);
        Task AddUser(CreateUserDto createUserDto);
        Task<bool> UpdateUser(int id, UpdateUserDto updateUserDto); 
        Task<bool> DeleteUser(int id);
        Task<bool> UserEmailExists(string email);
        Task<User> GetUserByEmail(string email);

    }
}
