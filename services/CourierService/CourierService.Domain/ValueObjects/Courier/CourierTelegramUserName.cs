using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Domain.Core;

namespace CourierService.Domain.ValueObjects.Courier;

/// <summary>
/// Представляет ник в телеграме.
/// </summary>
[DebuggerDisplay("{_telegramUserName}")]
public sealed class CourierTelegramUserName : ValueObject
{
    /// <summary>
    /// Представляет максимальную длину ника телеграма.
    /// </summary>
    public const int MaxLength = 250;

    private readonly string _telegramUserName;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="CourierTelegramUserName" />.
    /// </summary>
    /// <param name="telegramUserName">Ник в телеграмме.</param>
    public CourierTelegramUserName(string telegramUserName)
    {
        if (string.IsNullOrWhiteSpace(telegramUserName))
        {
            throw new ArgumentException("Ник в телеграмме не может быть null или пустой строкой.", nameof(telegramUserName));
        }

        if (!telegramUserName.StartsWith("@"))
        {
            telegramUserName = "@" + telegramUserName;
        }

        if (telegramUserName.Length > MaxLength)
        {
            throw new ArgumentException($"Ник в телеграмме не может быть больше {MaxLength}.", nameof(telegramUserName));
        }

        _telegramUserName = telegramUserName;
    }

    /// <summary>
    /// Возвращает набор компонентов, участвующий в сравнении.
    /// </summary>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return _telegramUserName;
    }

    /// <summary>
    /// Выполняет явное преобразование из <see cref="string" /> в <see cref="CourierTelegramUserName" />.
    /// </summary>
    /// <param name="obj">Ник в телеграмме.</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static explicit operator CourierTelegramUserName?(string? obj) => obj is null ? null : new CourierTelegramUserName(obj);

    /// <summary>
    /// Выполняет неявное преобразование из <see cref="CourierTelegramUserName" /> в <see cref="string" />.
    /// </summary>
    /// <param name="obj">Ник в телеграмме.</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static implicit operator string?(CourierTelegramUserName? obj) => obj?._telegramUserName;
}
