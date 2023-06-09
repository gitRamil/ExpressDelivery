using CourierService.Domain.Entities.Dictionaries;
using CourierService.Domain.ValueObjects.Dictionaries.Right;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourierService.Infrastructure.Persistence.Configurations.Dictionaries;

/// <summary>
/// Представляет настройку конфигурации для типа <see cref="Right" />.
/// </summary>
internal class RightConfiguration : EntityTypeConfigurationBase<Right>
{
    /// <summary>
    /// Вызывается при выполнении конфигурации сущности типа <see cref="Right" />.
    /// </summary>
    /// <param name="builder">Строитель, используемый при конфигурации сущности.</param>
    protected override void OnConfigure(EntityTypeBuilder<Right> builder)
    {
        builder.ToTable("rights", t => t.HasComment("Права"));

        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(RightName.MaxLength)
               .HasComment("Наименование")
               .HasConversion(o => (string)o, s => new RightName(s));

        builder.Property(p => p.Code)
               .IsRequired()
               .HasMaxLength(RightCode.MaxLength)
               .HasComment("Код")
               .HasConversion(o => (string)o, s => new RightCode(s));

        builder.HasIndex(p => p.Code)
               .IsUnique();

        builder.HasData(Right.Admin, Right.Courier, Right.User);
    }
}
