using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Domain.Core;

namespace CourierService.Domain.ValueObjects.Order;

/// <summary>
/// Представляет имя отправителя.
/// </summary>
[DebuggerDisplay("{_senderName}")]
public sealed class OrderSenderName : ValueObject
{
    /// <summary>
    /// Представляет максимальную имени отправителя.
    /// </summary>
    public const int MaxLength = 250;

    private readonly string _senderName;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="OrderSenderName" />.
    /// </summary>
    /// <param name="senderName">Имя отправителя.</param>
    public OrderSenderName(string senderName)
    {
        if (string.IsNullOrWhiteSpace(senderName))
        {
            throw new ArgumentException("Имя отправителя не может быть null или пустой строкой.", nameof(senderName));
        }

        senderName = senderName.Trim();

        if (senderName.Length > MaxLength)
        {
            throw new ArgumentException($"Имя отправителя не может быть больше {MaxLength}.", nameof(senderName));
        }

        _senderName = senderName;
    }

    /// <inheritdoc />
    public override string ToString() => _senderName;

    /// <summary>
    /// Возвращает набор компонентов, участвующий в сравнении.
    /// </summary>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return _senderName;
    }

    /// <summary>
    /// Выполняет явное преобразование из <see cref="string" /> в <see cref="OrderSenderName" />.
    /// </summary>
    /// <param name="obj">Имя отправителя.</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static explicit operator OrderSenderName?(string? obj) => obj is null ? null : new OrderSenderName(obj);

    /// <summary>
    /// Выполняет неявное преобразование из <see cref="OrderSenderName" /> в <see cref="string" />.
    /// </summary>
    /// <param name="obj">Имя отправителя.</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static implicit operator string?(OrderSenderName? obj) => obj?._senderName;
}
