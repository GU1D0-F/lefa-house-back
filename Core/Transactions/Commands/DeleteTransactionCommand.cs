using MediatR;

namespace Core.Transactions
{
    public class DeleteTransactionCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
