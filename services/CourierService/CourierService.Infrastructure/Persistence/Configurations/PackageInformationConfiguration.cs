using CourierService.Domain.Entities;
using CourierService.Domain.ValueObjects.PackageInformation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourierService.Infrastructure.Persistence.Configurations;

/// <summary>
/// Представляет настройку конфигурации для типа <see cref="PackageInformation" />.
/// </summary>
internal class PackageInformationConfiguration : EntityTypeConfigurationBase<PackageInformation>
{
    /// <summary>
    /// Вызывается при выполнении конфигурации сущности типа <see cref="PackageInformation" />.
    /// </summary>
    /// <param name="builder">Строитель, используемый при конфигурации сущности.</param>
    protected override void OnConfigure(EntityTypeBuilder<PackageInformation> builder)
    {
        builder.ToTable("package_information", t => t.HasComment("Информация о посылке"));

        builder.Property(p => p.Weight)
               .HasConversion(o => (int)o, s => new PackageInformationWeight(s))
               .HasComment("Вес посылки");

        builder.Property(p => p.Cost)
               .HasConversion(o => (int)o, s => new PackageInformationCost(s))
               .HasComment("Цена посылки");

        builder.Property(p => p.ShortDescription)
               .HasMaxLength(PackageInformationShortDescription.MaxLength)
               .HasConversion(o => (string)o, s => (PackageInformationShortDescription)s)
               .HasComment("Краткое описание");
    }
}
