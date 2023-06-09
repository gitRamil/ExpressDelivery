using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Domain.Core;

namespace CourierService.Domain.ValueObjects.PackageInformation;

/// <summary>
/// Представляет цену посылки.
/// </summary>
[DebuggerDisplay("{_cost}")]
public sealed class PackageInformationCost : ValueObject
{
    /// <summary>
    /// Представляет максимальную цену посылки.
    /// </summary>
    public const int MaxValue = 250;

    /// <summary>
    /// Представляет минимальную цену посылки.
    /// </summary>
    public const int MinValue = 0;

    private readonly decimal _cost;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="PackageInformationCost" />.
    /// </summary>
    /// <param name="cost">цену посылки.</param>
    public PackageInformationCost(decimal cost) =>
        _cost = cost switch
        {
            < MinValue => throw new ArgumentOutOfRangeException(nameof(cost), cost, $"Вес посылки не может быть меньше {MinValue}."),
            > MaxValue => throw new ArgumentOutOfRangeException(nameof(cost), cost, $"Вес посылки не может быть больше {MaxValue}."),
            _ => cost
        };

    /// <summary>
    /// Возвращает набор компонентов, участвующий в сравнении.
    /// </summary>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return _cost;
    }

    /// <summary>
    /// Выполняет явное преобразование из <see cref="decimal" /> в <see cref="PackageInformationCost" />.
    /// </summary>
    /// <param name="obj">Цена посылки посылки.</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static explicit operator PackageInformationCost?(decimal? obj) => obj is null ? null : new PackageInformationCost(obj.Value);

    /// <summary>
    /// Выполняет неявное преобразование из <see cref="PackageInformationCost" /> в <see cref="decimal" />.
    /// </summary>
    /// <param name="obj">Цена посылки посылки.</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static implicit operator decimal?(PackageInformationCost? obj) => obj?._cost;
}
