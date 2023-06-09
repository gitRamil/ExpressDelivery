using CourierService.Domain.Entities.Dictionaries;
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

        builder.Property(p => p.Code)
               .HasComment("Код");

        builder.Property(p => p.Name)
               .HasComment("Название");

        builder.HasData(Right.Admin, Right.Courier, Right.User);
    }
}
