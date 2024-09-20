using Core.Data;
using MediatR;

namespace Core.Payments
{
    internal class CreatePaymentCommandHandler(CoreDbContext context) : IRequestHandler<CreatePaymentCommand, int>
    {
        private readonly CoreDbContext _context = context;

        public async Task<int> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = new Payment
            {
                Amount = request.Amount,
                Description = request.Description,
                Category = request.Category,
                Date = request.Date,
                UserName = request.UserName
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync(cancellationToken);

            return payment.Id;
        }
    }
}
