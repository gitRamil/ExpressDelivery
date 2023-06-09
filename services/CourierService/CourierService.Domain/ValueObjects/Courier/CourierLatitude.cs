using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Domain.Core;

namespace CourierService.Domain.ValueObjects.Courier;

/// <summary>
/// Представляет координату широты курьера.
/// </summary>
[DebuggerDisplay("{_latitude}")]
public sealed class CourierLatitude : ValueObject
{
    /// <summary>
    /// Представляет максимальную длину координаты широты курьера.
    /// </summary>
    public const int MaxLength = 250;

    private readonly string _latitude;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="CourierLatitude" />.
    /// </summary>
    /// <param name="latitude">Координата широты курьера.</param>
    public CourierLatitude(string latitude)
    {
        if (string.IsNullOrWhiteSpace(latitude))
        {
            throw new ArgumentException("Координата широты курьера не может быть null или пустой строкой.", nameof(latitude));
        }
        
        latitude = latitude.Trim();

        if (latitude.Length > MaxLength)
        {
            throw new ArgumentException($"Координата широты курьера не может быть больше {MaxLength}.", nameof(latitude));
        }

        _latitude = latitude;
    }

    /// <summary>
    /// Возвращает набор компонентов, участвующий в сравнении.
    /// </summary>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return _latitude;
    }

    /// <summary>
    /// Выполняет явное преобразование из <see cref="string" /> в <see cref="CourierLatitude" />.
    /// </summary>
    /// <param name="obj">Координата широты курьера.</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static explicit operator CourierLatitude?(string? obj) => obj is null ? null : new CourierLatitude(obj);

    /// <summary>
    /// Выполняет неявное преобразование из <see cref="CourierLatitude" /> в <see cref="string" />.
    /// </summary>
    /// <param name="obj">Координата широты курьера.</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static implicit operator string?(CourierLatitude? obj) => obj?._latitude;
}
