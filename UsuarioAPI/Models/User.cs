using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace UserApi.Models
{
    [Table("usuarios")] 
    public class User
    {
        [Key]
        [Column("ID")] 
        public int ID { get; set; }

        [Required]
        [Column("nombre")] 
        public string Nombre { get; set; }

        [Required]
        [Column("email")] 
        public string Email { get; set; }

        [Required]
        [Column("password")] 
        public string Password { get; set; }
    }
}