using CourierService.Domain.Entities;
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

        builder.Property(p => p.Cost)
               .HasComment("Цена")
               .HasColumnType("decimal(18,2)");

        builder.Property(p => p.ShortDescription)
               .HasComment("Краткое описание");

        builder.Property(p => p.Weight)
               .HasComment("Вес")
               .HasColumnType("decimal(18,2)");
    }
}
