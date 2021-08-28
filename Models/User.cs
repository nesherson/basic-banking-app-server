using System.Collections.Generic;

namespace basic_banking_app_server.Models
{
    public partial class User
    {
        public User()
        {
            Cards = new HashSet<Card>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Card> Cards { get; set; }
    }
}
