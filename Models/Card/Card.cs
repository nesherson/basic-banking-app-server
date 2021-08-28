using basic_banking_app_server.Models.UserModel;

namespace basic_banking_app_server.Models.CardModel
{ 
    public partial class Card
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string Type { get; set; }
        public string Network { get; set; }
        public decimal? Balance { get; set; }
        public int? UserId { get; set; }

        public virtual User User { get; set; }
    }
}
