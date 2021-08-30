using System.ComponentModel.DataAnnotations;

namespace basic_banking_app_server.Dtos.TransactionDtos
{
    public class TransactionWithdrawCreateDto
    {
        [Required]
        public decimal Amount { get; set; }

        [Required]
        public int CardId { get; set; }
    }
}
