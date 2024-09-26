using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserApi.DTOs;
using UserApi.Models;
using UserApi.Services.Interfaces;

namespace UserApi.Controllers.AuthController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _config;

        public AuthController(IUserService userService, IConfiguration config)
        {
            _userService = userService;
            _config = config;
        }

        // registrar nuevos usuarios
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            // verificar si el email ya está en uso
            var emailExists = await _userService.UserEmailExists(registerDto.Email);
            if (emailExists)
            {
                return BadRequest("El email ya está en uso.");
            }

            // crea un nuevo CreateUserDto con los datos de registro
            var createUserDto = new CreateUserDto
            {
                Nombre = registerDto.Nombre,
                Email = registerDto.Email,
                Password = registerDto.Password 
            };

            // llama al servicio para que gestione el proceso de agregar usuario y encriptarlo
            await _userService.AddUser(createUserDto);

            return Ok("Usuario registrado exitosamente.");
        }

        // metodo para el login y generación de JWT
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            // buscar al usuario por email
            var user = await _userService.GetUserByEmail(loginDto.Email);

            //   verificar si el usuario existe
            if (user == null)
            {
                
                return Unauthorized("Email o contraseña incorrectos.");
            }


            // verificar la contraseña 
            var passwordCorrect = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password);

            if (!passwordCorrect)
            {

                return Unauthorized("Email o contraseña incorrectos.");
            }

            // generar el token JWT si la contraseña es correcta
            var token = GenerateJwtToken(user);

            return Ok(new { Token = token });
        }

        // metodo para generar el token JWT
        private string GenerateJwtToken(User user)
        {
            var key = Encoding.ASCII.GetBytes(_config.GetValue<string>("JwtConfig:Secret"));
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.Name, user.ID.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}