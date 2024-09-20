using Core.Data;
using Core.Expenses;
using Core.Payments;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace LefaHouseBack.Tests.Core.Expenses
{
    public class CreateExpenseCommandHandlerTests
    {
        private readonly Mock<CoreDbContext> _contextMock;
        private readonly Mock<DbSet<Expense>> _dbSetMock;

        public CreateExpenseCommandHandlerTests()
        {
            _contextMock = new();
            _dbSetMock = new();
            _contextMock.Setup(c => c.Expenses).Returns(_dbSetMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldCreateExpenseAndReturnId()
        {
            var command = new CreateExpenseCommand
            {
                Amount = 100,
                Description = "Test Payment",
                Category = "Test Category",
                Date = DateTime.Now,
                UserName = "Test User"
            };

            var handler = new CreateExpenseCommandHandler(_contextMock.Object);

            _dbSetMock.Setup(s => s.Add(It.IsAny<Expense>())).Callback<Expense>(p =>
            {
                p.Id = 1;
            });

            _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Act
            var paymentId = await handler.Handle(command, default);

            // Assert
            _dbSetMock.Verify(s => s.Add(It.IsAny<Expense>()), Times.Once);
            _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

            Assert.Equal(1, paymentId);
        }
    }
}
