using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Domain.Core;

namespace CourierService.Domain.ValueObjects.Order;

/// <summary>
/// Представляет стоимость доставки.
/// </summary>
[DebuggerDisplay("{_deliveryCost}")]
public sealed class OrderDeliveryCost : ValueObject
{
    /// <summary>
    /// Представляет максимальную стоимость доставки.
    /// </summary>
    public const decimal MaxValue = 10000;

    /// <summary>
    /// Представляет минимальную стоимость доставки.
    /// </summary>
    public const decimal MinValue = 10;

    private readonly decimal _deliveryCost;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="OrderDeliveryCost" />.
    /// </summary>
    /// <param name="cost">Стоимтость доставки.</param>
    public OrderDeliveryCost(decimal cost) =>
        _deliveryCost = cost switch
        {
            < MinValue => throw new ArgumentOutOfRangeException(nameof(cost), cost, $"Стоимтость доставки не может быть меньше {MinValue}."),
            > MaxValue => throw new ArgumentOutOfRangeException(nameof(cost), cost, $"Стоимтость доставки не может быть больше {MaxValue}."),
            _ => cost
        };

    /// <summary>
    /// Возвращает набор компонентов, участвующий в сравнении.
    /// </summary>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return _deliveryCost;
    }

    /// <summary>
    /// Выполняет явное преобразование из <see cref="decimal" /> в <see cref="OrderDeliveryCost" />.
    /// </summary>
    /// <param name="obj">Стоимтость доставки.</param>
    /// <returns>
    /// Результат преобразования.
    /// </returns>
    public static explicit operator OrderDeliveryCost?(decimal? obj) => obj == null ? null : new OrderDeliveryCost(obj.Value);

    /// <summary>
    /// Выполняет неявное преобразование из <see cref="OrderDeliveryCost" /> в <see cref="decimal" />.
    /// </summary>
    /// <param name="obj">Стоимтость доставки.</param>
    /// <returns>
    /// Результат преобразования.
    /// </returns>
    [return: NotNullIfNotNull(nameof(obj))]
    public static implicit operator decimal?(OrderDeliveryCost? obj) => obj?._deliveryCost;
}
