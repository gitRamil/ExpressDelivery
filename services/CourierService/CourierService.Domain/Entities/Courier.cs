using Domain.Core;
using Domain.Core.Primitives;

namespace CourierService.Domain.Entities;

/// <summary>
/// Представляет сущность курьера.
/// </summary>
public class Courier : Entity<SequentialGuid>
{
    public Courier(SequentialGuid id, User user, string telegramUserName)
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
    /// Возвращает координаты пользователя.
    /// </summary>
    public string? E { get; set; }

    /// <summary>
    /// Возвращает координаты пользователя.
    /// </summary>
    public string? S { get; set; }

    /// <summary>
    /// Возвращает пользователя.
    /// </summary>
    public string TelegramUserName { get; }

    /// <summary>
    /// Возвращает пользователя.
    /// </summary>
    public virtual User User { get; }
}
