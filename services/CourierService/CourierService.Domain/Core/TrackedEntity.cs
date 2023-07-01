using Domain.Core;

namespace CourierService.Domain.Core;


/// <summary>
/// Представляет Entity в терминологии Ddd.
/// </summary>
/// <typeparam name="TId">Тип идентификатора.</typeparam>
public abstract class TrackedEntity<TId> : Entity<TId>, ITrackedEntity
{
    /// <summary>
    /// Возвращает дату и время создания сущности.
    /// </summary>
    public DateTimeOffset CreatedDate { get; private set; }

    /// <summary>
    /// Устанавливает дату и время создания сущности.
    /// </summary>
    public void SetCreatedDate() => CreatedDate = DateTimeOffset.UtcNow;

    /// <summary>
    /// Устанавливает дату и время обновления сущности.
    /// </summary>
    public void SetUpdatedDate() => UpdatedDate = DateTimeOffset.UtcNow;

    /// <summary>
    /// Возвращает дату и время обновления сущности.
    /// </summary>
    public DateTimeOffset UpdatedDate { get; private set; }

    protected TrackedEntity(TId id)
        : base(id!)
    {
    }
}
