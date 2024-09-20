using Core.Data;
using MediatR;

namespace Core.Payments
{
    internal class DeletePaymentCommandHandler(CoreDbContext context) : IRequestHandler<DeletePaymentCommand, Unit>
    {
        private readonly CoreDbContext _context = context;

        public async Task<Unit> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = await _context.Payments.FindAsync(request.Id);

            if (payment != null)
            {
                _context.Payments.Remove(payment);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}
