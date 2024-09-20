using Core.Data;
using Core.Payments;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace LefaHouseBack.Tests.Core.Payments
{
    public class DeletePaymentCommandHandlerTests
    {
        private readonly Mock<CoreDbContext> _contextMock;
        private readonly Mock<DbSet<Payment>> _dbSetMock;

        public DeletePaymentCommandHandlerTests()
        {
            _contextMock = new();
            _dbSetMock = new();
            _contextMock.Setup(c => c.Payments).Returns(_dbSetMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldDeletePayment_WhenPaymentExists()
        {
            // Arrange
            var payment = new Payment { Id = 1 };
            var command = new DeletePaymentCommand { Id = 1 };

            _dbSetMock.Setup(s => s.FindAsync(It.IsAny<object[]>()))
                .ReturnsAsync(payment);

            var handler = new DeletePaymentCommandHandler(_contextMock.Object);

            // Act
            await handler.Handle(command, default);

            // Assert
            _dbSetMock.Verify(s => s.Remove(It.IsAny<Payment>()), Times.Once);
            _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldNotDeletePayment_WhenPaymentDoesNotExist()
        {
            // Arrange
            var command = new DeletePaymentCommand { Id = 1 };

            _dbSetMock.Setup(s => s.FindAsync(It.IsAny<object[]>()))
                .ReturnsAsync((Payment)null); // Payment não encontrado

            var handler = new DeletePaymentCommandHandler(_contextMock.Object);

            // Act
            await handler.Handle(command, default);

            // Assert
            _dbSetMock.Verify(s => s.Remove(It.IsAny<Payment>()), Times.Never);
            _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}