using CourierService.Domain.Entities.Dictionaries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourierService.Infrastructure.Persistence.Configurations.Dictionaries;

/// <summary>
/// Представляет настройку конфигурации для типа <see cref="OrderStatus" />.
/// </summary>
internal class OrderStatusConfiguration : EntityTypeConfigurationBase<OrderStatus>
{
    /// <summary>
    /// Вызывается при выполнении конфигурации сущности типа <see cref="OrderStatus" />.
    /// </summary>
    /// <param name="builder">Строитель, используемый при конфигурации сущности.</param>
    protected override void OnConfigure(EntityTypeBuilder<OrderStatus> builder)
    {
        builder.ToTable("orderStatuses", t => t.HasComment("Статус заказа"));

        builder.Property(p => p.Code)
               .HasComment("Код статуса");

        builder.Property(p => p.Name)
               .HasComment("Название статуса");

        builder.HasData(OrderStatus.CourierAssigned, OrderStatus.Created, OrderStatus.Done, OrderStatus.InProgress);
    }
}
