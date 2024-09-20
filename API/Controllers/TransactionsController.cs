using Core.Transactions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransactionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactionById(int id)
        {
            var query = new GetTransactionByIdQuery { Id = id };
            var transaction = await _mediator.Send(query);
            return transaction != null ? Ok(transaction) : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactions()
        {
            var query = new GetTransactionsQuery();
            var transactions = await _mediator.Send(query);
            return Ok(transactions);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionCommand command)
        {
            var transactionId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetTransactionById), new { id = transactionId }, command);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var command = new DeleteTransactionCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
