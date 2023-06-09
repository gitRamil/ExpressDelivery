using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Domain.Core;

namespace CourierService.Domain.ValueObjects.PackageInformation;

/// <summary>
/// Представляет вес посылки.
/// </summary>
[DebuggerDisplay("{_weight}")]
public sealed class PackageInformationWeight : ValueObject
{
    /// <summary>
    /// Представляет максимальный вес посылки.
    /// </summary>
    public const int MaxValue = 100;

    /// <summary>
    /// Представляет минимальный вес посылки.
    /// </summary>
    public const int MinValue = 0;

    private readonly int _weight;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="PackageInformationWeight" />.
    /// </summary>
    /// <param name="weight">Вес посылки.</param>
    public PackageInformationWeight(int weight) =>
        _weight = weight switch
        {
            < MinValue => throw new ArgumentOutOfRangeException(nameof(weight), weight, $"Вес посылки не может быть меньше {MinValue}."),
            > MaxValue => throw new ArgumentOutOfRangeException(nameof(weight), weight, $"Вес посылки не может быть больше {MaxValue}."),
            _ => weight
        };

    /// <summary>
    /// Возвращает набор компонентов, участвующий в сравнении.
    /// </summary>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return _weight;
    }

    /// <summary>
    /// Выполняет явное преобразование из <see cref="int" /> в <see cref="PackageInformationWeight" />.
    /// </summary>
    /// <param name="obj">Вес посылки.</param>
    /// <returns>
    /// Результат преобразования.
    /// </returns>
    public static explicit operator PackageInformationWeight?(int? obj) => obj == null ? null : new PackageInformationWeight(obj.Value);

    /// <summary>
    /// Выполняет неявное преобразование из <see cref="PackageInformationWeight" /> в <see cref="int" />.
    /// </summary>
    /// <param name="obj">Вес посылки.</param>
    /// <returns>
    /// Результат преобразования.
    /// </returns>
    [return: NotNullIfNotNull(nameof(obj))]
    public static implicit operator int?(PackageInformationWeight? obj) => obj?._weight;
}
