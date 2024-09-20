using Core.Expenses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExpensesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExpenseById(int id)
        {
            var query = new GetExpenseByIdQuery { Id = id };
            var expense = await _mediator.Send(query);
            return expense != null ? Ok(expense) : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetExpenses()
        {
            var query = new GetExpensesQuery();
            var expenses = await _mediator.Send(query);
            return Ok(expenses);
        }

        [HttpPost]
        public async Task<IActionResult> CreateExpense([FromBody] CreateExpenseCommand command)
        {
            var expenseId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetExpenseById), new { id = expenseId }, command);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var command = new DeleteExpenseCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
