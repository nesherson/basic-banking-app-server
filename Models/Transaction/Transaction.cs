using System;

using basic_banking_app_server.Models.CardModel;
using basic_banking_app_server.Enums;

namespace basic_banking_app_server.Models.TransactionModel
{
    public partial class Transaction
    {
        //public Transaction(int id, decimal amount, TransactionEnums.Method method, TransactionEnums.Status status, string description, DateTime createdAt, DateTime capturedAt, DateTime refundedAt, string senderCardNum, string receiverCardNum, int cardId)
        //{
        //    Id = id;
        //    Amount = amount;
        //    Method = method;
        //    Status = status;
        //    Description = description;
        //    CreatedAt = createdAt;
        //    CapturedAt = capturedAt;
        //    RefundedAt = refundedAt;
        //    SenderCardNum = senderCardNum;
        //    ReceiverCardNum = receiverCardNum;
        //    CardId = cardId;
        //}
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public TransactionEnums.Method Method { get; set; }
        public TransactionEnums.Status Status { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime CapturedAt { get; set; }
        public DateTime RefundedAt { get; set; }
        public string SenderCardNum { get; set; }
        public string ReceiverCardNum { get; set; }
        public int CardId { get; set; }
        public virtual Card Card { get; set; }
        public virtual Card ReceiverCardNumNavigation { get; set; }
        public virtual Card SenderCardNumNavigation { get; set; }
    }
}
