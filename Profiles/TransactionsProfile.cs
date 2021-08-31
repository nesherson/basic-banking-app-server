using AutoMapper;

using basic_banking_app_server.Models.TransactionModel;
using basic_banking_app_server.Dtos.TransactionDtos;
using basic_banking_app_server.Enums;

namespace basic_banking_app_server.Profiles
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        { 
            CreateMap<TransactionDepositCreateDto, Transaction>().ConstructUsing(e => new Transaction(e.Amount, TransactionEnums.Method.deposit, null, null, null, e.CardId));
            CreateMap<TransactionWithdrawCreateDto, Transaction>().ConstructUsing(e => new Transaction(e.Amount, TransactionEnums.Method.withdraw, null, null, null, e.CardId));
            CreateMap<TransactionPaymentCreateDto, Transaction>().ConstructUsing(e => new Transaction(e.Amount, TransactionEnums.Method.payment, e.Description, e.SenderCardNum, e.ReceiverCardNum, e.CardId));
            CreateMap<Transaction, TransactionDepositReadDto>();
            CreateMap<Transaction, TransactionWithdrawReadDto>();
            CreateMap<Transaction, TransactionPaymentReadDto>();
        }
    }
}
