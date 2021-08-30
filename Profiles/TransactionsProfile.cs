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
            CreateMap<TransactionDepositCreateDto, Transaction>().ConstructUsing(e => new Transaction(e.Amount, TransactionEnums.Method.deposit, e.CardId));
            CreateMap<TransactionWithdrawCreateDto, Transaction>().ConstructUsing(e => new Transaction(e.Amount, TransactionEnums.Method.withdraw, e.CardId));
            CreateMap<Transaction, TransactionDepositReadDto>();
            CreateMap<Transaction, TransactionWithdrawReadDto>();
        }
    }
}
