using MediatR;

namespace Core.Expenses
{
    public class GetExpenseByIdQuery : IRequest<ExpenseDto>
    {
        public int Id { get; set; }
    }
}
