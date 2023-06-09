using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Domain.Core;

namespace CourierService.Domain.ValueObjects.Courier;

/// <summary>
/// Представляет координату долготы курьера.
/// </summary>
[DebuggerDisplay("{_longitude}")]
public sealed class CourierLongitude : ValueObject
{
    /// <summary>
    /// Представляет максимальную длину координаты долготы курьера.
    /// </summary>
    public const int MaxLength = 250;

    private readonly string _longitude;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="CourierLongitude" />.
    /// </summary>
    /// <param name="longitude">Координата долготы курьера.</param>
    public CourierLongitude(string longitude)
    {
        if (string.IsNullOrWhiteSpace(longitude))
        {
            throw new ArgumentException("Координата долготы курьера не может быть null или пустой строкой.", nameof(longitude));
        }

        longitude = longitude.Trim();

        if (longitude.Length > MaxLength)
        {
            throw new ArgumentException($"Координата долготы курьера не может быть больше {MaxLength}.", nameof(longitude));
        }

        _longitude = longitude;
    }

    /// <summary>
    /// Возвращает набор компонентов, участвующий в сравнении.
    /// </summary>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return _longitude;
    }

    /// <summary>
    /// Выполняет явное преобразование из <see cref="string" /> в <see cref="CourierLongitude" />.
    /// </summary>
    /// <param name="obj">Координата долготы курьера.</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static explicit operator CourierLongitude?(string? obj) => obj is null ? null : new CourierLongitude(obj);

    /// <summary>
    /// Выполняет неявное преобразование из <see cref="CourierLongitude" /> в <see cref="string" />.
    /// </summary>
    /// <param name="obj">Координата долготы курьера.</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static implicit operator string?(CourierLongitude? obj) => obj?._longitude;
}
