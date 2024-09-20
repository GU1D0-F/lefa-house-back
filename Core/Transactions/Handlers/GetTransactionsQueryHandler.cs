using Core.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Transactions
{
    public class GetTransactionsQueryHandler(CoreDbContext context) : IRequestHandler<GetTransactionsQuery, List<TransactionDto>>
    {
        private readonly CoreDbContext _context = context;

        public async Task<List<TransactionDto>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Transactions
                .Select(t => new TransactionDto
                {
                    Id = t.Id,
                    Action = t.Action,
                    Amount = t.Amount,
                    Description = t.Description,
                    Category = t.Category,
                    Date = t.Date
                })
                .ToListAsync(cancellationToken);
        }
    }
}
