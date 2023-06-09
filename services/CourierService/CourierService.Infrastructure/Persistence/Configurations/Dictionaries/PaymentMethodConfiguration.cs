using CourierService.Domain.Entities.Dictionaries;
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
        builder.ToTable("paymentMethods", t => t.HasComment("Метод оплаты заказа"));

        builder.Property(p => p.Code)
               .HasComment("Код");

        builder.Property(p => p.Name)
               .HasComment("Название");

        builder.HasData(PaymentMethod.Card, PaymentMethod.Cash, PaymentMethod.Online);
    }
}
