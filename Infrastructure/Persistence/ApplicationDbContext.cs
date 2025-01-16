using Application.Data;
using Domain.Primitives;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence
{
  public class ApplicationDbContext : DbContext, IApplicationDbContext, IUnitOfWork
  {
    private readonly IPublisher _publisher;

    [ActivatorUtilitiesConstructor]
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IPublisher publisher)
            : base(options)
    {
      _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
    }

    // Construtor usado no tempo de design (fábrica)
    protected ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }


    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
      // Collect domain events before saving
      var domainEvents = ChangeTracker.Entries<AggregateRoot>()
          .Select(e => e.Entity)
          .Where(e => e.GetDomainEvents().Any())
          .SelectMany(e => e.GetDomainEvents())
          .ToList();

      // Save changes to the database
      var result = await base.SaveChangesAsync(cancellationToken);

      // Publish domain events
      foreach (var domainEvent in domainEvents)
      {
        try
        {
          await _publisher.Publish(domainEvent, cancellationToken);
        }
        catch (Exception ex)
        {
          // Handle or log the exception as needed
          throw new Exception("Error publishing domain event.", ex);
        }
      }
      return result;
    }
  }
}
