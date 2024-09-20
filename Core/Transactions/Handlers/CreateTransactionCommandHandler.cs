using Core.Data;
using MediatR;

namespace Core.Transactions
{
    public class CreateTransactionCommandHandler(CoreDbContext context) : IRequestHandler<CreateTransactionCommand, int>
    {
        private readonly CoreDbContext _context = context;

        public async Task<int> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = new Transaction
            {
                Action = request.Action,
                Amount = request.Amount,
                Description = request.Description,
                Category = request.Category,
                Date = request.Date
            };

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync(cancellationToken);

            return transaction.Id;
        }
    }
}
