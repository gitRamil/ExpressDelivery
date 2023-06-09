using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Domain.Core;

namespace CourierService.Domain.ValueObjects.Dictionaries.OrderStatus;

/// <summary>
/// Представляет код статуса заказа.
/// </summary>
[DebuggerDisplay("{_code}")]
public sealed class OrderStatusCode : ValueObject
{
    /// <summary>
    /// Представляет максимальную длину кода статуса заказа.
    /// </summary>
    public const int MaxLength = 100;

    private readonly string _code;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="OrderStatusCode" />.
    /// </summary>
    /// <param name="code">Код статуса заказа.</param>
    /// <exception cref="ArgumentException">
    /// Возникает, если <paramref name="code" />
    /// равен <c>null</c> или пустой строке или его длина превышает <see cref="MaxLength" />.
    /// </exception>
    public OrderStatusCode(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            throw new ArgumentException("Код статуса заказа не может быть null или пустой строкой.", nameof(code));
        }

        code = code.Trim();

        if (code.Length > MaxLength)
        {
            throw new ArgumentException($"Код статуса заказа не может быть больше {MaxLength}.", nameof(code));
        }

        _code = code;
    }

    /// <inheritdoc />
    public override string ToString() => _code;

    /// <summary>
    /// Возвращает набор компонентов, участвующий в сравнении.
    /// </summary>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return _code;
    }

    /// <summary>
    /// Выполняет неявное преобразование из <see cref="OrderStatusCode" /> в <see cref="string" />.
    /// </summary>
    /// <param name="obj">Код статуса заказа.</param>
    [return: NotNullIfNotNull("obj")]
    public static implicit operator string?(OrderStatusCode? obj) => obj?._code;
}
