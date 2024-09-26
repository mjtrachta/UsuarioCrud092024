using System.ComponentModel.DataAnnotations;

namespace UserApi.DTOs
{
    public class CreateUserDto
    {
        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}