using CourierService.Domain.Core;
using CourierService.Domain.ValueObjects.PackageInformation;
using Domain.Core;
using Domain.Core.Primitives;

namespace CourierService.Domain.Entities;

/// <summary>
/// Представляет сущность с информацией о посылке.
/// </summary>
public class PackageInformation : TrackedEntity<SequentialGuid>
{
    public PackageInformation(SequentialGuid id, PackageInformationShortDescription shortDescription, PackageInformationWeight weight, PackageInformationCost cost)
        : base(id)
    {
        ShortDescription = shortDescription ?? throw new ArgumentNullException(nameof(shortDescription));
        Weight = weight ?? throw new ArgumentNullException(nameof(weight));
        Cost = cost ?? throw new ArgumentNullException(nameof(cost));
    }

    /// <summary>
    /// Возвращает цену.
    /// </summary>
    public PackageInformationCost Cost { get; }

    /// <summary>
    /// Возвращает краткое описание.
    /// </summary>
    public PackageInformationShortDescription ShortDescription { get; }

    /// <summary>
    /// Возвращает вес.
    /// </summary>
    public PackageInformationWeight Weight { get; }
}
