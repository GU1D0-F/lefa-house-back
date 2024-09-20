using MediatR;

namespace Core.Payments
{
    public class GetPaymentByIdQuery : IRequest<PaymentDto>
    {
        public int Id { get; set; }
    }
}
