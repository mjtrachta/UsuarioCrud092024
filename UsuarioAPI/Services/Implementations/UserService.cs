using UserApi.DTOs;
using UserApi.Models;
using UserApi.Repositories.Interfaces;
using UserApi.Services.Interfaces;

namespace UserApi.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        
        public async Task<bool> UserEmailExists(string email)
        {
            return await _userRepository.UserEmailExists(email);
        }

        // buscar por email
        public async Task<User> GetUserByEmail(string email)
        {
            return await _userRepository.GetUserByEmail(email);
        }


        // buscar por id
        public async Task<User> GetUserById(int id)
        {
            return await _userRepository.GetUserById(id);
        }

        // crear usuario con bcrypt

        public async Task AddUser(CreateUserDto createUserDto)
        {
            //encriptar la pass utilizando bcrypt
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password);

           
            var user = new User
            {
                Nombre = createUserDto.Nombre,
                Email = createUserDto.Email,
                Password = hashedPassword 
            };

            
            await _userRepository.AddUser(user);
        }

        //actualizar con bcrypt

        public async Task<bool> UpdateUser(int id, UpdateUserDto updateUserDto)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return false;
            }

            user.Nombre = updateUserDto.Nombre;
            user.Email = updateUserDto.Email;
            user.Password = BCrypt.Net.BCrypt.HashPassword(updateUserDto.Password);

            await _userRepository.UpdateUser(user);
            return true;
        }

        // eliminar con token

        public async Task<bool> DeleteUser(int id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return false;
            }

            await _userRepository.DeleteUser(id);
            return true;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userRepository.GetAllUsers();
        }
    }

}
