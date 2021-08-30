using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System;

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
        public ActionResult Deposit(TransactionDepositRegisterDto depositCreateModel)
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
    }
}
