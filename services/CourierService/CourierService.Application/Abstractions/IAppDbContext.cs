using CourierService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourierService.Application.Abstractions;

/// <summary>
/// Описывает контекст взаимодействия с БД.
/// </summary>
public interface IAppDbContext
{
    /// <summary>
    /// Возвращает набор заказов.
    /// </summary>
    /// <value>
    /// Заказы.
    /// </value>
    DbSet<Order> Orders { get; }

    /// <summary>
    /// Возвращает набор пользователей.
    /// </summary>
    /// <value>
    /// Пользователи.
    /// </value>
    DbSet<User> Users { get; }

    /// <summary>
    /// Сохраняет изменения.
    /// </summary>
    /// <param name="cancellationToken">Маркер отмены.</param>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
