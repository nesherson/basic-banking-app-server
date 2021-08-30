using System.Collections.Generic;

using basic_banking_app_server.Models.UserModel;
using basic_banking_app_server.Models.TransactionModel;

namespace basic_banking_app_server.Models.CardModel
{ 
    public partial class Card
    {
        public Card()
        {
            TransactionCards = new HashSet<Transaction>();
            TransactionReceiverCardNumNavigations = new HashSet<Transaction>();
            TransactionSenderCardNumNavigations = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string Type { get; set; }
        public string Network { get; set; }
        public decimal Balance { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Transaction> TransactionCards { get; set; }
        public virtual ICollection<Transaction> TransactionReceiverCardNumNavigations { get; set; }
        public virtual ICollection<Transaction> TransactionSenderCardNumNavigations { get; set; }

    }
}
