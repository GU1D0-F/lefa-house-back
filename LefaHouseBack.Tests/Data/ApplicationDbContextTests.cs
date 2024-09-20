using Core.Data;
using Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Data.Tests
{
    public class ApplicationDbContextTests
    {
        private IServiceProvider CreateServiceProvider()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddApplicationDbContext(options =>
            {
                options.UseInMemoryDatabase("TestDatabase");
            });

            return serviceCollection.BuildServiceProvider();
        }

        [Fact]
        public void CanResolveApplicationDbContext()
        {
            var serviceProvider = CreateServiceProvider();

            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<CoreDbContext>();

                // Ensure the context can be created
                Assert.NotNull(dbContext);

                // Test if the DbSets are properly initialized
                Assert.NotNull(dbContext.Payments);
                Assert.NotNull(dbContext.Expenses);
                Assert.NotNull(dbContext.Transactions);
            }
        }
    }
}
