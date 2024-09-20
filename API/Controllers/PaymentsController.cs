using Core.Payments;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentById(int id)
        {
            var query = new GetPaymentByIdQuery { Id = id };
            var payment = await _mediator.Send(query);
            return payment != null ? Ok(payment) : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetPayments()
        {
            var query = new GetPaymentsQuery();
            var payments = await _mediator.Send(query);
            return Ok(payments);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentCommand command)
        {
            var paymentId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetPaymentById), new { id = paymentId }, command);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var command = new DeletePaymentCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
