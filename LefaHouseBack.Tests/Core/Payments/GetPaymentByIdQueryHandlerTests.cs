using Core.Data;
using Core.Payments;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LefaHouseBack.Tests.Core.Payments
{
    public class GetPaymentByIdQueryHandlerTests
    {
        private readonly Mock<CoreDbContext> _contextMock;
        private readonly Mock<DbSet<Payment>> _dbSetMock;

        public GetPaymentByIdQueryHandlerTests()
        {
            _contextMock = new();
            _dbSetMock = new();
            _contextMock.Setup(c => c.Payments).Returns(_dbSetMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnPaymentDto_WhenPaymentExists()
        {
            // Arrange
            var payment = new Payment
            {
                Id = 1,
                Amount = 100,
                Description = "Test Payment",
                Category = "Test Category",
                Date = DateTime.Now,
                UserName = "Test User"
            };

            var expectedDto = new PaymentDto
            {
                Id = payment.Id,
                Amount = payment.Amount,
                Description = payment.Description,
                Category = payment.Category,
                Date = payment.Date,
                UserName = payment.UserName
            };

            var query = new GetPaymentByIdQuery { Id = 1 };

            _contextMock.Setup(x => x.Payments)
                .ReturnsDbSet([payment]);

            var handler = new GetPaymentByIdQueryHandler(_contextMock.Object);

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
        public async Task Handle_ShouldReturnNull_WhenPaymentDoesNotExist()
        {
            // Arrange
            var query = new GetPaymentByIdQuery { Id = 1 };

            _contextMock.Setup(x => x.Payments)
                .ReturnsDbSet(Enumerable.Empty<Payment>()); // Mockando uma lista vazia de Payments

            var handler = new GetPaymentByIdQueryHandler(_contextMock.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert
            Assert.Null(result);
        }
    }
}