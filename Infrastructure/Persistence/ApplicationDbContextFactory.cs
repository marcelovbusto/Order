using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence
{
  public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
  {
    public ApplicationDbContext CreateDbContext(string[] args)
    {
      var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

      var configuration = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
          .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
          .Build();

      var connectionString = configuration.GetConnectionString("SqlServer");
      optionsBuilder.UseSqlServer(connectionString);

      // Usando o construtor protegido do DbContext
      return (ApplicationDbContext)Activator.CreateInstance(
          typeof(ApplicationDbContext),
          optionsBuilder.Options
      )!;
    }
  }
}
