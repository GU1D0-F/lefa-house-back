using Core.Data;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Extensions
{
    public static class ApplicationDbContextInjector
    {
        public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, Action<DbContextOptionsBuilder>? optionsAction = null)
        => services
            .AddScoped<CoreDbContext, ApplicationDbContext>()
            .AddDbContextFactory<CoreDbContext, ApplicationDbContextFactory>(optionsAction, ServiceLifetime.Scoped);
    }
}
