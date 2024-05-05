using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        ServiceCollection services = new();

        services.AddApplicationServices();
        services.AddInfrastructureServices(configuration);

        ServiceProvider serviceProvider = services.BuildServiceProvider();

        return ActivatorUtilities.CreateInstance<AppDbContext>(serviceProvider);
    }
}
