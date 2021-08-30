using System;
using System.Collections.Generic;

using basic_banking_app_server.Data.Context;
using basic_banking_app_server.Models.TransactionModel;
using basic_banking_app_server.Enums;

namespace basic_banking_app_server.Data.TransactionRepo
{
    public class TransactionRepo : ITransactionRepo
    {
        private readonly BasicBankContext _context;
 
        public TransactionRepo(BasicBankContext context)
        {
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

            //Transaction transactionDepositModel = new Transaction(default, transactionDeposit.Amount, TransactionEnums.Method.deposit, TransactionEnums.Status.captured, null, DateTime.Now, DateTime.Now, default, null, null, transactionDeposit.CardId);
            //transaction.CreatedAt = DateTime.Now;
            //transaction.CapturedAt = DateTime.Now;
            //transaction.Status = TransactionEnums.Status.captured;
            transactionDeposit.CreatedAt = DateTime.Now;
            transactionDeposit.CapturedAt = DateTime.Now;
            transactionDeposit.Status = TransactionEnums.Status.captured;
            transactionDeposit.Method = TransactionEnums.Method.deposit;
            _context.Transactions.Add(transactionDeposit);
            _context.SaveChanges();
        }
        public void MakePayment(Transaction transactionPayment)
        {
            throw new NotImplementedException();
        }

        public void MakeWithdraw(Transaction transactionWithdraw)
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
