using Core.Data;
using Core.Expenses;
using Core.Transactions;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace LefaHouseBack.Tests.Core.Transactions
{
    public class GetTransactionsQueryHandlerTests
    {
        private readonly Mock<CoreDbContext> _contextMock;
        private readonly Mock<DbSet<Transaction>> _dbSetMock;
        private readonly GetTransactionsQueryHandler _handler;


        public GetTransactionsQueryHandlerTests()
        {
            _contextMock = new();
            _dbSetMock = new();
            _contextMock.Setup(c => c.Transactions).Returns(_dbSetMock.Object);
            _handler = new GetTransactionsQueryHandler(_contextMock.Object);
        }


        [Fact]
        public async Task Handle_ShouldReturnListOfExpenses_WhenExpensesExist()
        {
            // Arrange
            var transactions = new List<Transaction>
            {
                new() { Id = 1, Amount = 100.00m, Description = "Test1", Category = "Category1", Date = DateTime.Now},
                new() { Id = 2, Amount = 200.00m, Description = "Test2", Category = "Category2", Date = DateTime.Now }
            };

            _contextMock.Setup(x => x.Transactions)
                .ReturnsDbSet(transactions);

            var query = new GetTransactionsQuery();

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
            _contextMock.Setup(x => x.Transactions)
                .ReturnsDbSet([]);

            var query = new GetTransactionsQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }
    }
}
