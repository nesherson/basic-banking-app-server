using System;

using basic_banking_app_server.Models.CardModel;

namespace basic_banking_app_server.Models.TransactionModel
{
    public partial class TransactionPayment
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public DateTime? CapturedAt { get; set; }
        public DateTime? RefundedAt { get; set; }
        public string SenderCardNumber { get; set; }
        public string ReceiverCardNumber { get; set; }

        public virtual Card ReceiverCardNumberNavigation { get; set; }
        public virtual Card SenderCardNumberNavigation { get; set; }
    }
}
