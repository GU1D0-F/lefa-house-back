using Core.Data;
using MediatR;

namespace Core.Expenses
{
    internal class CreateExpenseCommandHandler(CoreDbContext context) : IRequestHandler<CreateExpenseCommand, int>
    {
        private readonly CoreDbContext _context = context;

        public async Task<int> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
        {
            var expense = new Expense
            {
                Amount = request.Amount,
                Description = request.Description,
                Category = request.Category,
                Date = request.Date,
                UserName = request.UserName
            };

            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync(cancellationToken);

            return expense.Id;
        }
    }
}
