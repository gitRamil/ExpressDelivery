using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Domain.Core;

namespace CourierService.Domain.ValueObjects.Order;

/// <summary>
/// Представляет адрес получателя.
/// </summary>
[DebuggerDisplay("{_receiverAddress}")]
public sealed class OrderReceiverAddress : ValueObject
{
    /// <summary>
    /// Представляет максимальную длину адреса получателя.
    /// </summary>
    public const int MaxLength = 250;

    private readonly string _receiverAddress;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="OrderReceiverAddress" />.
    /// </summary>
    /// <param name="receiverAddress">Адрес получателя.</param>
    public OrderReceiverAddress(string receiverAddress)
    {
        if (string.IsNullOrWhiteSpace(receiverAddress))
        {
            throw new ArgumentException("Адрес получателя не может быть null или пустой строкой.", nameof(receiverAddress));
        }

        receiverAddress = receiverAddress.Trim();

        if (receiverAddress.Length > MaxLength)
        {
            throw new ArgumentException($"Адрес получателя не может быть больше {MaxLength}.", nameof(receiverAddress));
        }

        _receiverAddress = receiverAddress;
    }

    /// <inheritdoc />
    public override string ToString() => _receiverAddress;

    /// <summary>
    /// Возвращает набор компонентов, участвующий в сравнении.
    /// </summary>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return _receiverAddress;
    }

    /// <summary>
    /// Выполняет явное преобразование из <see cref="string" /> в <see cref="OrderReceiverAddress" />.
    /// </summary>
    /// <param name="obj">Адрес получателя.</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static explicit operator OrderReceiverAddress?(string? obj) => obj is null ? null : new OrderReceiverAddress(obj);

    /// <summary>
    /// Выполняет неявное преобразование из <see cref="OrderReceiverAddress" /> в <see cref="string" />.
    /// </summary>
    /// <param name="obj">Адрес получателя.</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static implicit operator string?(OrderReceiverAddress? obj) => obj?._receiverAddress;
}
