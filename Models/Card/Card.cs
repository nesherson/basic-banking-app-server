﻿using System.Collections.Generic;

using basic_banking_app_server.Models.UserModel;
using basic_banking_app_server.Models.TransactionModel;

namespace basic_banking_app_server.Models.CardModel
{ 
    public partial class Card
    {
        public Card()
        {
            TransactionDeposits = new HashSet<TransactionDeposit>();
            TransactionPaymentReceiverCardNumberNavigations = new HashSet<TransactionPayment>();
            TransactionPaymentSenderCardNumberNavigations = new HashSet<TransactionPayment>();
            TransactionWithdrawals = new HashSet<TransactionWithdrawal>();
        }

        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string Type { get; set; }
        public string Network { get; set; }
        public decimal? Balance { get; set; }
        public int? UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<TransactionDeposit> TransactionDeposits { get; set; }
        public virtual ICollection<TransactionPayment> TransactionPaymentReceiverCardNumberNavigations { get; set; }
        public virtual ICollection<TransactionPayment> TransactionPaymentSenderCardNumberNavigations { get; set; }
        public virtual ICollection<TransactionWithdrawal> TransactionWithdrawals { get; set; }
    }
}
