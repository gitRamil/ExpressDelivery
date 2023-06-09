using CourierService.Domain.Entities;
using CourierService.Domain.ValueObjects.Courier;
using CourierService.Domain.ValueObjects.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourierService.Infrastructure.Persistence.Configurations;

/// <summary>
/// Представляет настройку конфигурации для типа <see cref="Courier" />.
/// </summary>
internal class CourierConfiguration : EntityTypeConfigurationBase<Courier>
{
    /// <summary>
    /// Вызывается при выполнении конфигурации сущности типа <see cref="Courier" />.
    /// </summary>
    /// <param name="builder">Строитель, используемый при конфигурации сущности.</param>
    protected override void OnConfigure(EntityTypeBuilder<Courier> builder)
    {
        builder.ToTable("couriers", t => t.HasComment("Курьеры"));

        builder.Property(p => p.Longitude)
               .HasMaxLength(CourierLongitude.MaxLength)
               .HasConversion(o => (string?)o, s => (CourierLongitude?)s)
               .HasComment("Координаты долготы");

        builder.Property(p => p.Latitude)
               .HasMaxLength(CourierLatitude.MaxLength)
               .HasConversion(o => (string?)o, s => (CourierLatitude?)s)
               .HasComment("Координаты широты");

        builder.Property(p => p.TelegramUserName)
               .HasMaxLength(CourierTelegramUserName.MaxLength)
               .HasConversion(o => (string)o, s => (CourierTelegramUserName)s)
               .HasComment("Ник телеграмм");

        builder.HasOne(p => p.User)
               .WithMany()
               .HasForeignKey("user_id");

        builder.Property("user_id")
               .HasComment("Идентификатор пользователя");
    }
}
