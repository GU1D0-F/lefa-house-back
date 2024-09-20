using Core.Data;
using Core.Expenses;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace LefaHouseBack.Tests.Core.Expenses
{
    public class GetExpensesQueryHandlerTests
    {
        private readonly Mock<CoreDbContext> _contextMock;
        private readonly Mock<DbSet<Expense>> _dbSetMock;
        private readonly GetExpensesQueryHandler _handler;

        public GetExpensesQueryHandlerTests()
        {
            _contextMock = new();
            _dbSetMock = new();
            _contextMock.Setup(c => c.Expenses).Returns(_dbSetMock.Object);
            _handler = new GetExpensesQueryHandler(_contextMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnListOfExpenses_WhenExpensesExist()
        {
            // Arrange
            var expenses = new List<Expense>
            {
                new() { Id = 1, Amount = 100.00m, Description = "Test1", Category = "Category1", Date = DateTime.Now, UserName = "User1" },
                new() { Id = 2, Amount = 200.00m, Description = "Test2", Category = "Category2", Date = DateTime.Now, UserName = "User2" }
            };

            _contextMock.Setup(x => x.Expenses)
                .ReturnsDbSet(expenses);

            var query = new GetExpensesQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result[0].Id.Should().Be(1);
            result[1].Id.Should().Be(2);
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoExpenseExist()
        {
            // Arrange
            _contextMock.Setup(x => x.Expenses)
                .ReturnsDbSet([]);

            var query = new GetExpensesQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }
    }
}
