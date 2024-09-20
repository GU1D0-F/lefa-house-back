using Core.Data;
using Core.Payments;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace LefaHouseBack.Tests.Core.Payments
{
    public class CreatePaymentCommandHandlerTests
    {
        private readonly Mock<CoreDbContext> _contextMock;
        private readonly Mock<DbSet<Payment>> _dbSetMock;

        public CreatePaymentCommandHandlerTests()
        {
            _contextMock = new();
            _dbSetMock = new();
            _contextMock.Setup(c => c.Payments).Returns(_dbSetMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldCreatePaymentAndReturnId()
        {
            var command = new CreatePaymentCommand
            {
                Amount = 100,
                Description = "Test Payment",
                Category = "Test Category",
                Date = DateTime.Now,
                UserName = "Test User"
            };

            var handler = new CreatePaymentCommandHandler(_contextMock.Object);

            _dbSetMock.Setup(s => s.Add(It.IsAny<Payment>())).Callback<Payment>(p =>
            {
                p.Id = 1;
            });

            _contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(1);

            // Act
            var paymentId = await handler.Handle(command, default);

            // Assert
            _dbSetMock.Verify(s => s.Add(It.IsAny<Payment>()), Times.Once);
            _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

            Assert.Equal(1, paymentId);
        }
    }
}