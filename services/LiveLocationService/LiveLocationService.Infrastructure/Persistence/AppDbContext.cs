using LiveLocationService.Application.Abstractions;
using LiveLocationService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LiveLocationService.Infrastructure.Persistence;
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
        : base(options)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public DbSet<User> Users => Set<User>();

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
}
