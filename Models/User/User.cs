using System.Collections.Generic;

using basic_banking_app_server.Models.CardModel;

namespace basic_banking_app_server.Models.UserModel
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
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Card> Cards { get; set; }
    }
}
