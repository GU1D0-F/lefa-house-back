using MediatR;

namespace Core.Payments
{
    public class GetPaymentsQuery : IRequest<List<PaymentDto>>
    {
    }
}
