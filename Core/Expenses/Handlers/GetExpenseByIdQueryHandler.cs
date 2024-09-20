using Core.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Expenses
{
    internal class GetExpenseByIdQueryHandler(CoreDbContext context) : IRequestHandler<GetExpenseByIdQuery, ExpenseDto>
    {
        private readonly CoreDbContext _context = context;

        public async Task<ExpenseDto> Handle(GetExpenseByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Expenses
                .Where(e => e.Id == request.Id)
                .Select(e => new ExpenseDto
                {
                    Id = e.Id,
                    Amount = e.Amount,
                    Description = e.Description,
                    Category = e.Category,
                    Date = e.Date,
                    UserName = e.UserName
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
