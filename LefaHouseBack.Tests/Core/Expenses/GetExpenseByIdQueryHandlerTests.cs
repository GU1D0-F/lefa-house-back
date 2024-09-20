using Core.Data;
using Core.Expenses;
using Core.Payments;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace LefaHouseBack.Tests.Core.Expenses
{
    public class GetExpenseByIdQueryHandlerTests
    {
        private readonly Mock<CoreDbContext> _contextMock;
        private readonly Mock<DbSet<Expense>> _dbSetMock;

        public GetExpenseByIdQueryHandlerTests()
        {
            _contextMock = new();
            _dbSetMock = new();
            _contextMock.Setup(c => c.Expenses).Returns(_dbSetMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnExpenseDto_WhenExpenseExists()
        {
            // Arrange
            var expense = new Expense
            {
                Id = 1,
                Amount = 100,
                Description = "Test Payment",
                Category = "Test Category",
                Date = DateTime.Now,
                UserName = "Test User"
            };

            var expectedDto = new ExpenseDto
            {
                Id = expense.Id,
                Amount = expense.Amount,
                Description = expense.Description,
                Category = expense.Category,
                Date = expense.Date,
                UserName = expense.UserName
            };

            var query = new GetExpenseByIdQuery { Id = 1 };

            _contextMock.Setup(x => x.Expenses)
                .ReturnsDbSet([expense]);

            var handler = new GetExpenseByIdQueryHandler(_contextMock.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedDto.Id, result.Id);
            Assert.Equal(expectedDto.Amount, result.Amount);
            Assert.Equal(expectedDto.Description, result.Description);
            Assert.Equal(expectedDto.Category, result.Category);
            Assert.Equal(expectedDto.Date, result.Date);
            Assert.Equal(expectedDto.UserName, result.UserName);
        }




        [Fact]
        public async Task Handle_ShouldReturnNull_WhenExpenseDoesNotExist()
        {
            // Arrange
            var query = new GetExpenseByIdQuery { Id = 1 };

            _contextMock.Setup(x => x.Expenses)
                .ReturnsDbSet(Enumerable.Empty<Expense>()); // Mockando uma lista vazia de Payments

            var handler = new GetExpenseByIdQueryHandler(_contextMock.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            Assert.Null(result);
        }
    }
}
