using MediatR;

namespace Core.Expenses
{
    public class CreateExpenseCommand : IRequest<int>
    {
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
        public string UserName { get; set; }
    }
}
