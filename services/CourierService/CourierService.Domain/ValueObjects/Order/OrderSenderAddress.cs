using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Domain.Core;

namespace CourierService.Domain.ValueObjects.Order;

/// <summary>
/// Представляет адрес отправителя.
/// </summary>
[DebuggerDisplay("{_senderAddress}")]
public sealed class OrderSenderAddress : ValueObject
{
    /// <summary>
    /// Представляет максимальную длину адреса отправителя.
    /// </summary>
    public const int MaxLength = 250;

    private readonly string _senderAddress;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="OrderSenderAddress" />.
    /// </summary>
    /// <param name="senderAddress">Адрес отправителя.</param>
    public OrderSenderAddress(string senderAddress)
    {
        if (string.IsNullOrWhiteSpace(senderAddress))
        {
            throw new ArgumentException("Адрес отправителя не может быть null или пустой строкой.", nameof(senderAddress));
        }

        senderAddress = senderAddress.Trim();

        if (senderAddress.Length > MaxLength)
        {
            throw new ArgumentException($"Адрес отправителя не может быть больше {MaxLength}.", nameof(senderAddress));
        }

        _senderAddress = senderAddress;
    }

    /// <inheritdoc />
    public override string ToString() => _senderAddress;

    /// <summary>
    /// Возвращает набор компонентов, участвующий в сравнении.
    /// </summary>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return _senderAddress;
    }

    /// <summary>
    /// Выполняет явное преобразование из <see cref="string" /> в <see cref="OrderSenderAddress" />.
    /// </summary>
    /// <param name="obj">Адрес отправителя.</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static explicit operator OrderSenderAddress?(string? obj) => obj is null ? null : new OrderSenderAddress(obj);

    /// <summary>
    /// Выполняет неявное преобразование из <see cref="OrderSenderAddress" /> в <see cref="string" />.
    /// </summary>
    /// <param name="obj">Адрес отправителя.</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static implicit operator string?(OrderSenderAddress? obj) => obj?._senderAddress;
}
