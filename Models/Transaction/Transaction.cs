using System;

using basic_banking_app_server.Models.CardModel;
using basic_banking_app_server.Enums;

namespace basic_banking_app_server.Models.TransactionModel
{
    public partial class Transaction
    {
        public Transaction(decimal amount, TransactionEnums.Method method, string description, string senderCardNum, string receiverCardNum, int cardId)
        { 
            _amount = amount;
            _method = method;
            Status = TransactionEnums.Status.captured;
            _description = description;
            CreatedAt = DateTime.Now;
            CapturedAt = DateTime.Now;
            _senderCardNum = senderCardNum;
            _receiverCardNum = receiverCardNum;
            _cardId = cardId;
        }

        private decimal _amount;
        private TransactionEnums.Method _method;
        private string _description;
        private string _senderCardNum;
        private string _receiverCardNum;
        private int _cardId;
        public int Id { get; set; }
        public decimal Amount { get { return _amount; } }
        public TransactionEnums.Method Method { get { return _method;  } }
        public TransactionEnums.Status Status { get; set; }
        public string Description { get { return _description;  } }
        public DateTime CreatedAt { get; set; }
        public DateTime CapturedAt { get; set; }
        public DateTime? RefundedAt { get; set; }
        public string SenderCardNum { get { return _senderCardNum; } }
        public string ReceiverCardNum { get { return _receiverCardNum; } }
        public int CardId { get { return _cardId; } }
        public virtual Card Card { get; set; }
        public virtual Card ReceiverCardNumNavigation { get; set; }
        public virtual Card SenderCardNumNavigation { get; set; }
    }
}
