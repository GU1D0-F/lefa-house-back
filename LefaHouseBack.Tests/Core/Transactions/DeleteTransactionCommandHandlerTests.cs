using Core.Data;
using Core.Transactions;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace LefaHouseBack.Tests.Core.Transactions
{
    public class DeleteTransactionCommandHandlerTests
    {
        private readonly Mock<CoreDbContext> _contextMock;
        private readonly Mock<DbSet<Transaction>> _dbSetMock;

        public DeleteTransactionCommandHandlerTests()
        {
            _contextMock = new();
            _dbSetMock = new();
            _contextMock.Setup(c => c.Transactions).Returns(_dbSetMock.Object);
        }


        [Fact]
        public async Task Handle_ShouldDeleteExpense_WhenExpenseExists()
        {
            // Arrange
            var transaction = new Transaction { Id = 1 };
            var command = new DeleteTransactionCommand { Id = 1 };

            _dbSetMock.Setup(s => s.FindAsync(It.IsAny<object[]>()))
                .ReturnsAsync(transaction);

            var handler = new DeleteTransactionCommandHandler(_contextMock.Object);

            // Act
            await handler.Handle(command, default);

            // Assert
            _dbSetMock.Verify(s => s.Remove(It.IsAny<Transaction>()), Times.Once);
            _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldNotDeleteExpense_WhenExpenseDoesNotExist()
        {
            // Arrange
            var command = new DeleteTransactionCommand { Id = 1 };

            _dbSetMock.Setup(s => s.FindAsync(It.IsAny<object[]>()))
                .ReturnsAsync((Transaction)null); // Expense não encontrada

            var handler = new DeleteTransactionCommandHandler(_contextMock.Object);

            // Act
            await handler.Handle(command, default);

            // Assert
            _dbSetMock.Verify(s => s.Remove(It.IsAny<Transaction>()), Times.Never);
            _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
