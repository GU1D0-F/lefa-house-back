using MediatR;

namespace Core.Expenses
{
    public class DeleteExpenseCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
