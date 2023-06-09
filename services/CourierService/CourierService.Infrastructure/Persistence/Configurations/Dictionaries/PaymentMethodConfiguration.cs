using CourierService.Domain.Entities.Dictionaries;
using CourierService.Domain.ValueObjects.Dictionaries.PaymentMethod;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourierService.Infrastructure.Persistence.Configurations.Dictionaries;

/// <summary>
/// Представляет настройку конфигурации для типа <see cref="PaymentMethod" />.
/// </summary>
internal class PaymentMethodConfiguration : EntityTypeConfigurationBase<PaymentMethod>
{
    /// <summary>
    /// Вызывается при выполнении конфигурации сущности типа <see cref="PaymentMethod" />.
    /// </summary>
    /// <param name="builder">Строитель, используемый при конфигурации сущности.</param>
    protected override void OnConfigure(EntityTypeBuilder<PaymentMethod> builder)
    {
        builder.ToTable("payment_methods", t => t.HasComment("Метод оплаты заказа"));

        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(PaymentMethodName.MaxLength)
               .HasComment("Наименование")
               .HasConversion(o => (string)o, s => new PaymentMethodName(s));

        builder.Property(p => p.Code)
               .IsRequired()
               .HasMaxLength(PaymentMethodCode.MaxLength)
               .HasComment("Код")
               .HasConversion(o => (string)o, s => new PaymentMethodCode(s));

        builder.HasIndex(p => p.Code)
               .IsUnique();

        builder.HasData(PaymentMethod.Card, PaymentMethod.Cash, PaymentMethod.Online);
    }
}
