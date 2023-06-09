using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Domain.Core;

namespace CourierService.Domain.ValueObjects.Order;

/// <summary>
/// Представляет трек номер посылки.
/// </summary>
[DebuggerDisplay("{_trackNumber}")]
public sealed class OrderTrackNumber : ValueObject
{
    /// <summary>
    /// Представляет максимальную длину трек номера посылки.
    /// </summary>
    public const int MaxLength = 250;

    private readonly string _trackNumber;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="OrderTrackNumber" />.
    /// </summary>
    /// <param name="trackNumber">Трек номер посылки.</param>
    public OrderTrackNumber(string trackNumber)
    {
        if (string.IsNullOrWhiteSpace(trackNumber))
        {
            throw new ArgumentException("Трек номер посылки не может быть null или пустой строкой.", nameof(trackNumber));
        }

        trackNumber = trackNumber.Trim();

        if (trackNumber.Length > MaxLength)
        {
            throw new ArgumentException($"Трек номер посылки не может быть больше {MaxLength}.", nameof(trackNumber));
        }

        _trackNumber = trackNumber;
    }

    /// <summary>
    /// Возвращает набор компонентов, участвующий в сравнении.
    /// </summary>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return _trackNumber;
    }

    /// <summary>
    /// Выполняет явное преобразование из <see cref="string" /> в <see cref="OrderTrackNumber" />.
    /// </summary>
    /// <param name="obj">Трек номер посылки.</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static explicit operator OrderTrackNumber?(string? obj) => obj is null ? null : new OrderTrackNumber(obj);

    /// <summary>
    /// Выполняет неявное преобразование из <see cref="OrderTrackNumber" /> в <see cref="string" />.
    /// </summary>
    /// <param name="obj">Трек номер посылки.</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static implicit operator string?(OrderTrackNumber? obj) => obj?._trackNumber;
}
