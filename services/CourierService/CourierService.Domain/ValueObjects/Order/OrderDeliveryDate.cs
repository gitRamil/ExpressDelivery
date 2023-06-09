using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Domain.Core;

namespace CourierService.Domain.ValueObjects.Order;

/// <summary>
/// Представляет дату доставки.
/// </summary>
[DebuggerDisplay("{_deliveryDate}")]
public sealed class OrderDeliveryDate : ValueObject
{
    private readonly DateTime _deliveryDate;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="OrderDeliveryDate" />.
    /// </summary>
    /// <param name="deliveryDate">Дата увольнения.</param>
    public OrderDeliveryDate(DateTime deliveryDate) => _deliveryDate = deliveryDate;

    /// <summary>
    /// Возвращает набор компонентов, участвующий в сравнении.
    /// </summary>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return _deliveryDate;
    }

    /// <summary>
    /// Выполняет явное преобразование из <see cref="OrderDeliveryDate" /> в <see cref="DateTime" />.
    /// </summary>
    /// <param name="obj">Дата увольнения.</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static explicit operator OrderDeliveryDate?(DateTime? obj) => obj == null ? null : new OrderDeliveryDate(obj.Value);

    /// <summary>
    /// Выполняет неявное преобразование из <see cref="OrderDeliveryDate" /> в <see cref="DateTime" />.
    /// </summary>
    /// <param name="obj">Дата увольнения.</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static implicit operator DateTime?(OrderDeliveryDate? obj) => obj?._deliveryDate;
}
