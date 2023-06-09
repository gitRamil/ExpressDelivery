using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Domain.Core;

namespace CourierService.Domain.ValueObjects.PackageInformation;

/// <summary>
/// Представляет краткое описание посылки.
/// </summary>
[DebuggerDisplay("{_shortDescription}")]
public sealed class PackageInformationShortDescription : ValueObject
{
    /// <summary>
    /// Представляет максимальную длину описания посылки.
    /// </summary>
    public const int MaxLength = 250;

    private readonly string _shortDescription;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="PackageInformationShortDescription" />.
    /// </summary>
    /// <param name="shortDescription">Описание посылки.</param>
    public PackageInformationShortDescription(string shortDescription)
    {
        if (string.IsNullOrWhiteSpace(shortDescription))
        {
            throw new ArgumentException("Описание посылки не может быть null или пустой строкой.", nameof(shortDescription));
        }
        
        shortDescription = shortDescription.Trim();

        if (shortDescription.Length > MaxLength)
        {
            throw new ArgumentException($"Описание посылки не может быть больше {MaxLength}.", nameof(shortDescription));
        }

        _shortDescription = shortDescription;
    }

    /// <summary>
    /// Возвращает набор компонентов, участвующий в сравнении.
    /// </summary>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return _shortDescription;
    }

    /// <summary>
    /// Выполняет явное преобразование из <see cref="string" /> в <see cref="PackageInformationShortDescription" />.
    /// </summary>
    /// <param name="obj">Описание посылки.</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static explicit operator PackageInformationShortDescription?(string? obj) => obj is null ? null : new PackageInformationShortDescription(obj);

    /// <summary>
    /// Выполняет неявное преобразование из <see cref="PackageInformationShortDescription" /> в <see cref="string" />.
    /// </summary>
    /// <param name="obj">Описание посылки.</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static implicit operator string?(PackageInformationShortDescription? obj) => obj?._shortDescription;
}
