using Core.Data;
using MediatR;

namespace Core.Expenses
{
    internal class DeleteExpenseCommandHandler(CoreDbContext context) : IRequestHandler<DeleteExpenseCommand, Unit>
    {
        private readonly CoreDbContext _context = context;

        public async Task<Unit> Handle(DeleteExpenseCommand request, CancellationToken cancellationToken)
        {
            var expense = await _context.Expenses.FindAsync(request.Id);

            if (expense != null)
            {
                _context.Expenses.Remove(expense);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}
