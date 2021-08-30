using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System;
using System.Collections.Generic;

using basic_banking_app_server.Data.TransactionRepo;
using basic_banking_app_server.Dtos.TransactionDtos;
using basic_banking_app_server.Models.TransactionModel;

namespace basic_banking_app_server.Controllers.Transactions
{
    [Route("transactions")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionRepo _transactionRepo;
        private readonly IMapper _mapper;

        public TransactionsController(ITransactionRepo transactionRepo, IMapper mapper)
        {
            _transactionRepo = transactionRepo;
            _mapper = mapper;
        }

        [HttpPost("deposit")]
        public ActionResult Deposit(TransactionDepositCreateDto depositCreateModel)
        {
            var deposit = _mapper.Map<Transaction>(depositCreateModel);
            try
            {
                _transactionRepo.MakeDeposit(deposit);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return Ok();
        }

        [HttpGet("deposit")]
        public ActionResult<IEnumerable<TransactionDepositReadDto>> GetAllDepositTransactions()
        {
            var transactionsDeposit = _transactionRepo.GetAllDepositTransactions();
            var transactionsDepositReadDto = _mapper.Map<IEnumerable<TransactionDepositReadDto>>(transactionsDeposit);

            return Ok(transactionsDepositReadDto);
        }

        [HttpPost("withdraw")]
        public ActionResult Withdraw(TransactionWithdrawCreateDto withdrawCreateModel)
        {
            var withdraw = _mapper.Map<Transaction>(withdrawCreateModel);

            try
            {
                _transactionRepo.MakeWithdraw(withdraw);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return Ok();
        }

        [HttpGet("withdraw")]
        public ActionResult<IEnumerable<TransactionWithdrawReadDto>> GetAllWithdrawTransactions()
        {
            var listOfWithdrawTransactions = _transactionRepo.GetAllWithdrawTransactions();
            var listOfWithdrawTransactionsReadDto = _mapper.Map<IEnumerable<TransactionWithdrawReadDto>>(listOfWithdrawTransactions);

            return Ok(listOfWithdrawTransactionsReadDto);
        }

        [HttpPost("payment")]
        public ActionResult Payment(TransactionPaymentCreateDto paymentCreateModel)
        {
            var payment = _mapper.Map<Transaction>(paymentCreateModel);

            try
            {
                _transactionRepo.MakePayment(payment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return Ok();
        }
    }
}
