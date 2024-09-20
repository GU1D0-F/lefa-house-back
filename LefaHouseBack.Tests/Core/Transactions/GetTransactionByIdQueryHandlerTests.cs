using Core.Data;
using Core.Expenses;
using Core.Transactions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace LefaHouseBack.Tests.Core.Transactions
{
    public class GetTransactionByIdQueryHandlerTests
    {
        private readonly Mock<CoreDbContext> _contextMock;
        private readonly Mock<DbSet<Transaction>> _dbSetMock;

        public GetTransactionByIdQueryHandlerTests()
        {
            _contextMock = new();
            _dbSetMock = new();
            _contextMock.Setup(c => c.Transactions).Returns(_dbSetMock.Object);
        }


        [Fact]
        public async Task Handle_ShouldReturnExpenseDto_WhenExpenseExists()
        {
            // Arrange
            var transaction = new Transaction
            {
                Id = 1,
                Action = "Test",
                Amount = 100,
                Description = "Test transaction"
            };

            var expectedDto = new TransactionDto
            {
                Id = transaction.Id,
                Action = transaction.Action,
                Amount = transaction.Amount,
                Description = transaction.Description
            };

            var query = new GetTransactionByIdQuery { Id = 1 };

            _contextMock.Setup(x => x.Transactions)
                .ReturnsDbSet([transaction]);

            var handler = new GetTransactionByIdQueryHandler(_contextMock.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedDto.Id, result.Id);
            Assert.Equal(expectedDto.Amount, result.Amount);
            Assert.Equal(expectedDto.Description, result.Description);
            Assert.Equal(expectedDto.Category, result.Category);
            Assert.Equal(expectedDto.Date, result.Date);
        }




        [Fact]
        public async Task Handle_ShouldReturnNull_WhenExpenseDoesNotExist()
        {
            // Arrange
            var query = new GetTransactionByIdQuery { Id = 1 };

            _contextMock.Setup(x => x.Transactions)
                .ReturnsDbSet([]); // Mockando uma lista vazia de Payments

            var handler = new GetTransactionByIdQueryHandler(_contextMock.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            Assert.Null(result);
        }
    }
}
