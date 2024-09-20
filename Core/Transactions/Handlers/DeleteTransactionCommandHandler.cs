using Core.Data;
using MediatR;

namespace Core.Transactions
{
    public class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionCommand, Unit>
    {
        private readonly CoreDbContext _context;

        public DeleteTransactionCommandHandler(CoreDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = await _context.Transactions.FindAsync(request.Id);

            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}
