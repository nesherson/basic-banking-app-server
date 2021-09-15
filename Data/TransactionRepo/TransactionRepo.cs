using System;
using System.Linq;
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
        public void MakeDeposit(Transaction transactionDeposit)
        {
            if (transactionDeposit == null)
                throw new ArgumentNullException(nameof(transactionDeposit));

            if (transactionDeposit.Amount <= 0)
                throw new ArgumentException();

            TransactionEnums.Method methodDeposit = TransactionEnums.Method.deposit;

            Transaction transactionDepositModel = new Transaction(transactionDeposit.Amount, methodDeposit, null, null, null, transactionDeposit.CardId);
            _context.Transactions.Add(transactionDepositModel);
            _context.SaveChanges();
        }
        public void MakeWithdraw(Transaction transactionWithdraw)
        {
            if (transactionWithdraw == null)
                throw new ArgumentNullException(nameof(transactionWithdraw));

            if (transactionWithdraw.Amount <= 0)
                throw new ArgumentException();

            TransactionEnums.Method methodWithdraw = TransactionEnums.Method.withdraw;
            Transaction transactionWithdrawModel = new Transaction(transactionWithdraw.Amount, methodWithdraw, null, null, null, transactionWithdraw.CardId);
            _context.Transactions.Add(transactionWithdrawModel);
            _context.SaveChanges();
        }
        public void MakePayment(Transaction transactionPayment)
        {
            if (transactionPayment == null)
                throw new ArgumentNullException(nameof(transactionPayment));

            if (transactionPayment.Amount <= 0)
                throw new ArgumentException();

            TransactionEnums.Method methodPayment = TransactionEnums.Method.payment;
            Transaction transactionPaymentModel = new Transaction(transactionPayment.Amount, methodPayment, transactionPayment.Description, transactionPayment.SenderCardNum, transactionPayment.ReceiverCardNum, transactionPayment.CardId);
            _context.Transactions.Add(transactionPaymentModel);
            _context.SaveChanges();
        }
        public IEnumerable<Transaction> GetAllDepositTransactions()
        {
            var listOfDepositTransactions = _context.Transactions
                .Where(transaction => transaction.Method == TransactionEnums.Method.deposit)
                .ToList();

            return listOfDepositTransactions;
        }
        public IEnumerable<Transaction> GetAllWithdrawTransactions()
        {
            var listOfWithdrawTransactions = _context.Transactions
                .Where(transaction => transaction.Method == TransactionEnums.Method.withdraw)
                .ToList();

            return listOfWithdrawTransactions;
        }
        public IEnumerable<Transaction> GetAllPaymentTransactions()
        {
            var listOfPaymentTransactions = _context.Transactions
                .Where(transaction => transaction.Method == TransactionEnums.Method.payment)
                .ToList();

            return listOfPaymentTransactions;
        }
        public IEnumerable<Transaction> GetAllTransactions()
        {
            var listOfAllTransactions = _context.Transactions.ToList();

            return listOfAllTransactions;
        }
       

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public IEnumerable<Transaction> GetAllDepositTransactionsByCardId(int cardId)
        {
            var transactions = _context.Transactions
                .Where(t => t.CardId == cardId && t.Method == TransactionEnums.Method.deposit)
                .ToList();

            return transactions;
        }

        public IEnumerable<Transaction> GetAllWithdrawTransactionsByCardId(int cardId)
        {
            var transactions = _context.Transactions
               .Where(t => t.CardId == cardId && t.Method == TransactionEnums.Method.withdraw)
               .ToList();

            return transactions;
        }

        public IEnumerable<Transaction> GetAllPaymentTransactionsByCardNum(string cardNum)
        {
            var transactions = _context.Transactions
               .Where(t => t.SenderCardNum == cardNum || t.ReceiverCardNum == cardNum)
               .ToList();

            return transactions;
        }
        public IEnumerable<Transaction> GetLatestTransactionsByCardIdOrCardNum(int cardId, string cardNum, int resultLimit) 
        {
            if (resultLimit == 0)
            {
                var transactions = _context.Transactions
               .Where(t => t.CardId == cardId || t.SenderCardNum == cardNum || t.ReceiverCardNum == cardNum)
               .OrderByDescending(t => t.CreatedAt)
               .ToList();
                return transactions;
            }
            else
            {
                var transactions = _context.Transactions
               .Where(t => t.CardId == cardId || t.SenderCardNum == cardNum || t.ReceiverCardNum == cardNum)
               .OrderByDescending(t => t.CreatedAt)
               .Take(resultLimit)
               .ToList();
                return transactions;
            }
        }

        public IEnumerable<Transaction> GetLastMonthTransactionsByCardIdOrCardNum(int cardId, string cardNum, DateTime dateLimit)
        {
                var transactions = _context.Transactions
               .Where(t => t.CardId == cardId || t.SenderCardNum == cardNum || t.ReceiverCardNum == cardNum)
               .Where(t => t.CreatedAt >= dateLimit)
               .OrderByDescending(t => t.CreatedAt)
               .ToList();
                return transactions;   
        }
    }
}
