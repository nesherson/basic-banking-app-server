using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

using basic_banking_app_server.Data.TransactionRepo;
using basic_banking_app_server.Dtos.TransactionDtos;
using basic_banking_app_server.Models.TransactionModel;
using basic_banking_app_server.Data.CardRepo;

namespace basic_banking_app_server.Controllers.Transactions
{
    [Authorize]
    [Route("transactions")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionRepo _transactionRepo;
        private readonly ICardRepo _cardRepo;
        private readonly IMapper _mapper;

        public TransactionsController(ITransactionRepo transactionRepo, ICardRepo cardRepo, IMapper mapper)
        {
            _transactionRepo = transactionRepo;
            _cardRepo = cardRepo;
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
            var transactions = _transactionRepo.GetAllDepositTransactions();
            var transactionsReadDto = _mapper.Map<IEnumerable<TransactionDepositReadDto>>(transactions);

            return Ok(transactionsReadDto);
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
            var transactions = _transactionRepo.GetAllWithdrawTransactions();
            var transactionsReadDto = _mapper.Map<IEnumerable<TransactionWithdrawReadDto>>(transactions);

            return Ok(transactionsReadDto);
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

        [HttpGet("payment")]
        public ActionResult<IEnumerable<TransactionPaymentReadDto>> GetAllPaymentTransactions()
        {
            var transactions = _transactionRepo.GetAllPaymentTransactions();
            var transactionsReadDto = _mapper.Map<IEnumerable<TransactionPaymentReadDto>>(transactions);

            return Ok(transactionsReadDto);
        }

        [HttpGet("{cardId}/latest")]
        public ActionResult GetLatestTransactions(int cardId, [FromQuery] int resultLimit)
        {
            var card = _cardRepo.GetCardById(cardId);
            var transactions = _transactionRepo.GetLatestTransactionsByCardIdOrCardNum(cardId, card.CardNumber, resultLimit);

            if (transactions == null)
                return NotFound();

            var transactionsReadDto = _mapper.Map<IEnumerable<TransactionGeneralReadDto>>(transactions);

            return Ok(transactionsReadDto);
        }



    }
}
