using System.ComponentModel.DataAnnotations;

namespace basic_banking_app_server.Dtos.AuthDto
{
    public partial class UserAuthDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
