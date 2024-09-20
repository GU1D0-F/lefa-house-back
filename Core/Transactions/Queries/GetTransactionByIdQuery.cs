using MediatR;

namespace Core.Transactions
{
    public class GetTransactionByIdQuery : IRequest<TransactionDto>
    {
        public int Id { get; set; }
    }
}
