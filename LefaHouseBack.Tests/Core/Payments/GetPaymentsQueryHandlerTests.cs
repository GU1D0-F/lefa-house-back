using Core.Data;
using Core.Payments;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace LefaHouseBack.Tests.Core.Payments
{
    public class GetPaymentsQueryHandlerTests
    {
        private readonly Mock<CoreDbContext> _contextMock;
        private readonly Mock<DbSet<Payment>> _dbSetMock;
        private readonly GetPaymentsQueryHandler _handler;

        public GetPaymentsQueryHandlerTests()
        {
            _contextMock = new();
            _dbSetMock = new();
            _contextMock.Setup(c => c.Payments).Returns(_dbSetMock.Object);
            _handler = new GetPaymentsQueryHandler(_contextMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnListOfPayments_WhenPaymentsExist()
        {
            // Arrange
            var payments = new List<Payment>
            {
                new() { Id = 1, Amount = 100.00m, Description = "Test1", Category = "Category1", Date = DateTime.Now, UserName = "User1" },
                new() { Id = 2, Amount = 200.00m, Description = "Test2", Category = "Category2", Date = DateTime.Now, UserName = "User2" }
            };

            _contextMock.Setup(x => x.Payments)
                .ReturnsDbSet(payments);

            var query = new GetPaymentsQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result[0].Id.Should().Be(1);
            result[1].Id.Should().Be(2);
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoPaymentsExist()
        {
            // Arrange
            _contextMock.Setup(x => x.Payments)
                .ReturnsDbSet(new List<Payment>());

            var query = new GetPaymentsQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }
    }
}