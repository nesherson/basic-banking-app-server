using System;

namespace basic_banking_app_server.Dtos.TransactionDtos
{
    public class TransactionPaymentCreateDto
    {
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string SenderCardNum { get; set; }
        public string ReceiverCardNum { get; set; }
        public int CardId { get; set; }
    }
}
