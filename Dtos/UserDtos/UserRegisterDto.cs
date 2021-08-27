using System.ComponentModel.DataAnnotations;

namespace basic_banking_app_server.Dtos.AuthDto
{
    public partial class UserRegisterDto
    {
        [Required]
        [MaxLength(30)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
