using MediatR;

namespace Core.Expenses
{
    public class GetExpensesQuery : IRequest<List<ExpenseDto>>
    {
    }
}
