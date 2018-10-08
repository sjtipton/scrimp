﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using scrimp.Dtos;
using scrimp.Entities;
using scrimp.Helpers;
using scrimp.Services;
using System.Collections.Generic;

namespace scrimp.Controllers
{
    [ApiController]
    [Route("api")]
    public class TransactionsController : ControllerBase
    {
        private ITransactionService _transactionService;
        private IMapper _mapper;

        public TransactionsController(ITransactionService transactionService, IMapper mapper)
        {
            _transactionService = transactionService;
            _mapper = mapper;
        }

        // GET api/transaction_accounts/:id/transactions
        [HttpGet]
        [Route("transaction_accounts/{id}/transactions")]
        public IActionResult GetTransactionAccountTransactions(int id)
        {
            var transactionAccountTransactions = _transactionService.GetTransactionAccountTransactions(id);
            var transactionAccountTransactionDtos = _mapper.Map<IEnumerable<TransactionDto>>(transactionAccountTransactions);
            return Ok(transactionAccountTransactionDtos);
        }

        // GET api/accounts/:id/transactions
        [HttpGet]
        [Route("accounts/{id}/transactions")]
        public IActionResult GetAccountTransactions(int id)
        {
            var accountTransactions = _transactionService.GetAccountTransactions(id);
            var accountTransactionDtos = _mapper.Map<IEnumerable<TransactionDto>>(accountTransactions);
            return Ok(accountTransactionDtos);
        }

        // GET api/users/:id/transactions
        [HttpGet]
        [Route("users/{id}/transactions")]
        public IActionResult GetUserTransactions(int id)
        {
            var userTransactions = _transactionService.GetUserTransactions(id);
            var userTransactionDtos = _mapper.Map<IEnumerable<TransactionDto>>(userTransactions);
            return Ok(userTransactionDtos);
        }

        // POST api/transaction_accounts/:id/transactions
        [HttpPost]
        [Route("transaction_accounts/{id}/transactions")]
        public IActionResult CreateTransactionAccountTransaction(int id, [FromBody]TransactionDto transactionDto)
        {
            var transaction = _mapper.Map<Transaction>(transactionDto);

            try
            {
                _transactionService.CreateTransactionAccountTransaction(id, transaction);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET api/transactions/:id
        [HttpGet("transactions/{id}")]
        public IActionResult GetById(int id)
        {
            var transaction = _transactionService.GetById(id);
            var transactionDto = _mapper.Map<TransactionDto>(transaction);
            return Ok(transactionDto);
        }

        // PUT api/transactions/:id
        [HttpPut("transactions/{id}")]
        public IActionResult Update(int id, [FromBody]TransactionDto transactionDto)
        {
            var transaction = _mapper.Map<Transaction>(transactionDto);
            transaction.Id = id;

            try
            {
                _transactionService.Update(transaction);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
