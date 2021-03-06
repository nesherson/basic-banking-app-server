using System.ComponentModel.DataAnnotations;

namespace basic_banking_app_server.Dtos.UserDto
{
    public partial class UserUpdateDto
    {
        
        [MaxLength(30)]
        public string Email { get; set; }

     
        [MaxLength(20)]
        public string FirstName { get; set; }

        
        [MaxLength(30)]
        public string LastName { get; set; }

        
        //[MaxLength(50)]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
