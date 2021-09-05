using System.Collections.Generic;

using basic_banking_app_server.Models.TransactionModel;

namespace basic_banking_app_server.Data.TransactionRepo
{
    public interface ITransactionRepo
    {
        IEnumerable<Transaction> GetAllDepositTransactions();
        IEnumerable<Transaction> GetAllWithdrawTransactions();
        IEnumerable<Transaction> GetAllPaymentTransactions();
        IEnumerable<Transaction> GetAllDepositTransactionsByCardId(int cardId);
        IEnumerable<Transaction> GetAllWithdrawTransactionsByCardId(int cardId);
        IEnumerable<Transaction> GetAllPaymentTransactionsByCardNum(string cardNum);
        IEnumerable<Transaction> GetLatestTransactionsByCardIdOrCardNum(int cardId, string cardNum, int resultLimit);
        IEnumerable<Transaction> GetAllTransactions();
        void MakeDeposit(Transaction transactionDeposit);
        void MakeWithdraw(Transaction transactionWithdraw);
        void MakePayment(Transaction transactionPayment);
        bool SaveChanges();
    }
}
