using CourierService.Application.Abstractions;
using CourierService.Domain.Core;
using CourierService.Domain.Entities;
using CourierService.Domain.Entities.Dictionaries;
using Microsoft.EntityFrameworkCore;

namespace CourierService.Infrastructure.Persistence;

/// <summary>
/// Представляет контекст для работы с БД.
/// </summary>
/// <seealso cref="DbContext" />
public sealed class AppDbContext : DbContext, IAppDbContext
{
    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="AppDbContext" />.
    /// </summary>
    /// <param name="options">Опции.</param>
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) =>
        AttachDictionaryValues();

    /// <inheritdoc />
    public DbSet<Order> Orders => Set<Order>();

    /// <inheritdoc cref="DbContext" />
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        foreach (var entry in ChangeTracker.Entries()
                                           .Where(x => x.Entity is ITrackedEntity))
        {
            if (entry.State == EntityState.Added)
            {
                ((ITrackedEntity)entry.Entity).SetCreatedDate();
                ((ITrackedEntity)entry.Entity).SetUpdatedDate();
            }
            else if (entry.State == EntityState.Modified)
            {
                ((ITrackedEntity)entry.Entity).SetUpdatedDate();
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public DbSet<User> Users => Set<User>();

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

    private void AttachDictionaryValues()
    {
        AttachRange(OrderStatus.GetAllValues());
        AttachRange(PaymentMethod.GetAllValues());
        AttachRange(Right.GetAllValues());
    }
}
