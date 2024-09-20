using Core.Data;
using Core.Expenses;
using Core.Payments;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LefaHouseBack.Tests.Core.Expenses
{
    public class DeleteExpenseCommandHandlerTests
    {
        private readonly Mock<CoreDbContext> _contextMock;
        private readonly Mock<DbSet<Expense>> _dbSetMock;

        public DeleteExpenseCommandHandlerTests()
        {
            _contextMock = new();
            _dbSetMock = new();
            _contextMock.Setup(c => c.Expenses).Returns(_dbSetMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldDeleteExpense_WhenExpenseExists()
        {
            // Arrange
            var expense = new Expense { Id = 1 };
            var command = new DeleteExpenseCommand { Id = 1 };

            _dbSetMock.Setup(s => s.FindAsync(It.IsAny<object[]>()))
                .ReturnsAsync(expense);

            var handler = new DeleteExpenseCommandHandler(_contextMock.Object);

            // Act
            await handler.Handle(command, default);

            // Assert
            _dbSetMock.Verify(s => s.Remove(It.IsAny<Expense>()), Times.Once);
            _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldNotDeleteExpense_WhenExpenseDoesNotExist()
        {
            // Arrange
            var command = new DeleteExpenseCommand { Id = 1 };

            _dbSetMock.Setup(s => s.FindAsync(It.IsAny<object[]>()))
                .ReturnsAsync((Expense)null); // Expense não encontrada

            var handler = new DeleteExpenseCommandHandler(_contextMock.Object);

            // Act
            await handler.Handle(command, default);

            // Assert
            _dbSetMock.Verify(s => s.Remove(It.IsAny<Expense>()), Times.Never);
            _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
