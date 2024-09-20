using MediatR;

namespace Core.Transactions
{
    public class CreateTransactionCommand : IRequest<int>
    {
        public string Action { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
    }
}
