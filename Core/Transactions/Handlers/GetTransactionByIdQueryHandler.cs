using Core.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Transactions
{
    public class GetTransactionByIdQueryHandler(CoreDbContext context) : IRequestHandler<GetTransactionByIdQuery, TransactionDto>
    {
        private readonly CoreDbContext _context = context;

        public async Task<TransactionDto> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Transactions
                .Where(t => t.Id == request.Id)
                .Select(t => new TransactionDto
                {
                    Id = t.Id,
                    Action = t.Action,
                    Amount = t.Amount,
                    Description = t.Description,
                    Category = t.Category,
                    Date = t.Date
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
