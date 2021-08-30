using System;

using basic_banking_app_server.Models.CardModel;

namespace basic_banking_app_server.Models.TransactionModel
{
    public partial class Transaction
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? CapturedAt { get; set; }
        public DateTime? RefundedAt { get; set; }
        public string SenderCardNum { get; set; }
        public string ReceiverCardNum { get; set; }
        public int? CardId { get; set; }

        public virtual Card Card { get; set; }
        public virtual Card ReceiverCardNumNavigation { get; set; }
        public virtual Card SenderCardNumNavigation { get; set; }
    }
}
