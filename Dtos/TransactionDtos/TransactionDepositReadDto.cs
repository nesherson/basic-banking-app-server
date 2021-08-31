using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using basic_banking_app_server.Enums;

namespace basic_banking_app_server.Dtos.TransactionDtos
{
    public class TransactionDepositReadDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public TransactionEnums.Method Method { get; set; }
        public TransactionEnums.Status Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime CapturedAt { get; set; }
        public int CardId { get; set; }
    }
}
