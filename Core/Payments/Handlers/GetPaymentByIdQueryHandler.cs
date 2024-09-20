using Core.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Payments
{
    internal class GetPaymentByIdQueryHandler(CoreDbContext context) : IRequestHandler<GetPaymentByIdQuery, PaymentDto>
    {
        private readonly CoreDbContext _context = context;

        public async Task<PaymentDto> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Payments
                .Where(p => p.Id == request.Id)
                .Select(p => new PaymentDto
                {
                    Id = p.Id,
                    Amount = p.Amount,
                    Description = p.Description,
                    Category = p.Category,
                    Date = p.Date,
                    UserName = p.UserName
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
