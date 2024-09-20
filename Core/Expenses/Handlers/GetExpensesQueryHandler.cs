using Core.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Expenses
{
    internal class GetExpensesQueryHandler(CoreDbContext context) : IRequestHandler<GetExpensesQuery, List<ExpenseDto>>
    {
        private readonly CoreDbContext _context = context;

        public async Task<List<ExpenseDto>> Handle(GetExpensesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Expenses
                .Select(e => new ExpenseDto
                {
                    Id = e.Id,
                    Amount = e.Amount,
                    Description = e.Description,
                    Category = e.Category,
                    Date = e.Date,
                    UserName = e.UserName
                })
                .ToListAsync(cancellationToken);
        }
    }
}
