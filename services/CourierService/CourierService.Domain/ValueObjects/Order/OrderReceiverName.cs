using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Domain.Core;

namespace CourierService.Domain.ValueObjects.Order;

/// <summary>
/// Представляет имя получателя.
/// </summary>
[DebuggerDisplay("{_receiverName}")]
public sealed class OrderReceiverName : ValueObject
{
    /// <summary>
    /// Представляет максимальную имени получателя.
    /// </summary>
    public const int MaxLength = 250;

    private readonly string _receiverName;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="OrderReceiverName" />.
    /// </summary>
    /// <param name="receiverName">Имя получателя.</param>
    public OrderReceiverName(string receiverName)
    {
        if (string.IsNullOrWhiteSpace(receiverName))
        {
            throw new ArgumentException("Имя получателя не может быть null или пустой строкой.", nameof(receiverName));
        }

        receiverName = receiverName.Trim();

        if (receiverName.Length > MaxLength)
        {
            throw new ArgumentException($"Имя получателя не может быть больше {MaxLength}.", nameof(receiverName));
        }

        _receiverName = receiverName;
    }

    /// <summary>
    /// Возвращает набор компонентов, участвующий в сравнении.
    /// </summary>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return _receiverName;
    }

    /// <summary>
    /// Выполняет явное преобразование из <see cref="string" /> в <see cref="OrderReceiverName" />.
    /// </summary>
    /// <param name="obj">Имя получателя.</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static explicit operator OrderReceiverName?(string? obj) => obj is null ? null : new OrderReceiverName(obj);

    /// <summary>
    /// Выполняет неявное преобразование из <see cref="OrderReceiverName" /> в <see cref="string" />.
    /// </summary>
    /// <param name="obj">Имя получателя.</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static implicit operator string?(OrderReceiverName? obj) => obj?._receiverName;
}
