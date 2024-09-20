using MediatR;

namespace Core.Payments
{
    public class DeletePaymentCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
