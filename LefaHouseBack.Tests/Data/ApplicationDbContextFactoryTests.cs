using Data.Context;
using Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Data.Tests
{
    public class ApplicationDbContextFactoryTests
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
        public void CanCreateDbContextUsingFactory()
        {
            var serviceProvider = CreateServiceProvider();
            var factory = new ApplicationDbContextFactory(serviceProvider);

            var dbContext = factory.CreateDbContext();

            // Ensure the context can be created
            Assert.NotNull(dbContext);

            // Test if the DbSets are properly initialized
            Assert.NotNull(dbContext.Payments);
            Assert.NotNull(dbContext.Expenses);
            Assert.NotNull(dbContext.Transactions);
        }
    }
}
