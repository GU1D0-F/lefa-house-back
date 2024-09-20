using Core.Data;
using Core.Expenses;
using Core.Transactions;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace LefaHouseBack.Tests.Core.Transactions
{
    public class CreateTransactionCommandHandlerTests
    {
        private readonly Mock<CoreDbContext> _contextMock;
        private readonly Mock<DbSet<Transaction>> _dbSetMock;

        public CreateTransactionCommandHandlerTests()
        {
            _contextMock = new();
            _dbSetMock = new();
            _contextMock.Setup(c => c.Transactions).Returns(_dbSetMock.Object);
        }


        [Fact]
        public async Task Handle_Should_Add_Transaction_To_Database()
        {
            var command = new CreateTransactionCommand
            {
                Action = "Deposit",
                Amount = 100,
                Description = "Test transaction",
                Category = "Category1",
                Date = DateTime.Now
            };

            var handler = new CreateTransactionCommandHandler(_contextMock.Object);

            _dbSetMock.Setup(s => s.Add(It.IsAny<Transaction>())).Callback<Transaction>(p =>
            {
                p.Id = 1;
            });

            _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Act
            var paymentId = await handler.Handle(command, default);

            // Assert
            _dbSetMock.Verify(s => s.Add(It.IsAny<Transaction>()), Times.Once);
            _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

            Assert.Equal(1, paymentId);
        }
    }
}
