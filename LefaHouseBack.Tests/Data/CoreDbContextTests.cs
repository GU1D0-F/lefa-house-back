using Core.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Data.Tests
{
    public class CoreDbContextTests
    {
        private DbContextOptions<CoreDbContext> CreateInMemoryDatabaseOptions()
        {
            return new DbContextOptionsBuilder<CoreDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
        }

        [Fact]
        public void CanConnectToDatabase()
        {
            var options = CreateInMemoryDatabaseOptions();

            using (var context = new CoreDbContext(options))
            {
                // Ensure the context can be created
                Assert.NotNull(context);

                // Test if the DbSets are properly initialized
                Assert.NotNull(context.Payments);
                Assert.NotNull(context.Expenses);
                Assert.NotNull(context.Transactions);
            }
        }
    }
}
