using System;
using System.Collections.Generic;
using AutoMapper;


using basic_banking_app_server.Data.Context;
using basic_banking_app_server.Models.TransactionModel;
using basic_banking_app_server.Enums;
using basic_banking_app_server.Dtos.TransactionDtos;

namespace basic_banking_app_server.Data.TransactionRepo
{
    public class TransactionRepo : ITransactionRepo
    {
        private readonly BasicBankContext _context;
        private readonly IMapper _mapper;
 
        public TransactionRepo(BasicBankContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public IEnumerable<Transaction> GetAllDepositTransactions()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Transaction> GetAllPaymentTransactions()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Transaction> GetAllWithdrawalTransactions()
        {
            throw new NotImplementedException();
        }

        public void MakeDeposit(Transaction transactionDeposit)
        {
            if (transactionDeposit == null)
                throw new ArgumentNullException(nameof(transactionDeposit));

            if (transactionDeposit.Amount <= 0)
                throw new ArgumentException();

            TransactionEnums.Method methodDeposit = TransactionEnums.Method.deposit;

            Transaction transactionDepositModel = new Transaction(transactionDeposit.Amount, methodDeposit, transactionDeposit.CardId);
            _context.Transactions.Add(transactionDepositModel);
            _context.SaveChanges();
        }
        public void MakePayment(Transaction transactionPayment)
        {
            throw new NotImplementedException();
        }

        public void MakeWithdraw(Transaction transactionWithdraw)
        {
            if (transactionWithdraw == null)
                throw new ArgumentNullException(nameof(transactionWithdraw));

            if (transactionWithdraw.Amount <= 0)
                throw new ArgumentException();

            TransactionEnums.Method methodWithdraw = TransactionEnums.Method.withdraw;
            Transaction transactionWithdrawModel = new Transaction(transactionWithdraw.Amount, methodWithdraw, transactionWithdraw.CardId);
            _context.Transactions.Add(transactionWithdrawModel);
            _context.SaveChanges();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
