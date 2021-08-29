using System;

using basic_banking_app_server.Models.CardModel;

namespace basic_banking_app_server.Models.TransactionModel
{
    public partial class TransactionWithdrawal
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
        public DateTime CapturedAt { get; set; }
        public int CardId { get; set; }

        public virtual Card Card { get; set; }
    }
}
