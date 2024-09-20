using Core.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Payments
{
    internal class GetPaymentsQueryHandler(CoreDbContext context) : IRequestHandler<GetPaymentsQuery, List<PaymentDto>>
    {
        private readonly CoreDbContext _context = context;

        public async Task<List<PaymentDto>> Handle(GetPaymentsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Payments
                .Select(p => new PaymentDto
                {
                    Id = p.Id,
                    Amount = p.Amount,
                    Description = p.Description,
                    Category = p.Category,
                    Date = p.Date,
                    UserName = p.UserName
                })
                .ToListAsync(cancellationToken);
        }
    }
}
