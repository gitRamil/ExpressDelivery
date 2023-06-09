using Domain.Core;
using Domain.Core.Primitives;

namespace CourierService.Domain.Entities;

/// <summary>
/// Представляет сущность с информацией о посылке.
/// </summary>
public class PackageInformation : Entity<SequentialGuid>
{
    public PackageInformation(SequentialGuid id, string shortDescription, decimal weight, decimal cost)
        : base(id)
    {
        ShortDescription = shortDescription ?? throw new ArgumentNullException(nameof(shortDescription));
        Weight = weight;
        Cost = cost;
    }

    /// <summary>
    /// Возвращает цену.
    /// </summary>
    public decimal Cost { get; }

    /// <summary>
    /// Возвращает краткое описание.
    /// </summary>
    public string ShortDescription { get; }

    /// <summary>
    /// Возвращает вес.
    /// </summary>
    public decimal Weight { get; }
}
