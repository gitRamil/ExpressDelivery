using CourierService.Domain.Core;
using CourierService.Domain.ValueObjects.Courier;
using Domain.Core.Primitives;

namespace CourierService.Domain.Entities;

/// <summary>
/// Представляет сущность курьера.
/// </summary>
public class Courier : TrackedEntity<SequentialGuid> 
{
    public Courier(SequentialGuid id, User user, CourierTelegramUserName telegramUserName)
        : base(id)
    {
        User = user ?? throw new ArgumentNullException(nameof(user));
        TelegramUserName = telegramUserName ?? throw new ArgumentNullException(nameof(telegramUserName));
    }

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="Courier" />.
    /// </summary>
    /// <remarks>Конструктор для EF.</remarks>
    protected Courier()
        : base(SequentialGuid.Empty)
    {
        User = null!;
        TelegramUserName = null!;
    }

    /// <summary>
    /// Возвращает координату широты курьера.
    /// </summary>
    public CourierLatitude? Latitude { get; set; }

    /// <summary>
    /// Возвращает координату долготы курьера.
    /// </summary>
    public CourierLongitude? Longitude { get; set; }

    /// <summary>
    /// Возвращает пользователя.
    /// </summary>
    public CourierTelegramUserName TelegramUserName { get; }

    /// <summary>
    /// Возвращает пользователя.
    /// </summary>
    public virtual User User { get; }
}
