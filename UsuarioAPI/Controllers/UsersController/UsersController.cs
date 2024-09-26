using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserApi.DTOs;
using UserApi.Models;
using UserApi.Services.Interfaces;

namespace UserApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService; 

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // obtener todos los usuarios
        [HttpGet]
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userService.GetAllUsers();
        }

        // obtener  usuario por iD

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            
            var user = await _userService.GetUserById(id);
            if (user == null) return NotFound();
            return user;
        }

        //modificar

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UpdateUserDto updateUserDto)
        {
            var result = await _userService.UpdateUser(id, updateUserDto); 
            if (!result) return NotFound(); 

            return NoContent(); 
        }
        // eliminar con autorizacion

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            await _userService.DeleteUser(id);
            return NoContent();
        }
    }
}