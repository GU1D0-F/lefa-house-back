using MediatR;

namespace Core.Transactions
{
    public class GetTransactionsQuery : IRequest<List<TransactionDto>>
    {
    }
}
