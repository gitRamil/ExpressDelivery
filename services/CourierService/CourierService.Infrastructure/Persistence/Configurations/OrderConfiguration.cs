using CourierService.Domain.Entities;
using CourierService.Domain.ValueObjects.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourierService.Infrastructure.Persistence.Configurations;

/// <summary>
/// Представляет настройку конфигурации для типа <see cref="Order" />.
/// </summary>
internal class OrderConfiguration : EntityTypeConfigurationBase<Order>
{
    /// <summary>
    /// Вызывается при выполнении конфигурации сущности типа <see cref="Order" />.
    /// </summary>
    /// <param name="builder">Строитель, используемый при конфигурации сущности.</param>
    protected override void OnConfigure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("orders", t => t.HasComment("Заказ"));

        builder.Property(p => p.DeliveryCost)
               .HasConversion(o => (decimal?)o, s => (OrderDeliveryCost?)s)
               .HasComment("Цена доставки");

        builder.Property(p => p.DeliveryDate)
               .HasConversion(o => (DateTime?)o, s => (OrderDeliveryDate?)s)
               .HasComment("Дата доставки");

        builder.Property(p => p.ReceiverAddress)
               .HasMaxLength(OrderReceiverAddress.MaxLength)
               .HasConversion(o => (string)o, s => (OrderReceiverAddress)s)
               .HasComment("Адрес получателя");

        builder.Property(p => p.ReceiverName)
               .HasMaxLength(OrderReceiverName.MaxLength)
               .HasConversion(o => (string)o, s => (OrderReceiverName)s)
               .HasComment("Имя получателя");

        builder.Property(p => p.SenderAddress)
               .HasMaxLength(OrderSenderAddress.MaxLength)
               .HasConversion(o => (string)o, s => (OrderSenderAddress)s)
               .HasComment("Адрес отправителя");

        builder.Property(p => p.SenderName)
               .HasMaxLength(OrderSenderName.MaxLength)
               .HasConversion(o => (string)o, s => (OrderSenderName)s)
               .HasComment("Имя отправителя");

        builder.Property(p => p.TrackNumber)
               .HasConversion(p => (Guid)p, p => p)
               .HasComment("Номер отслеживания");

        builder.HasOne(p => p.Courier)
               .WithMany()
               .HasForeignKey("courier_id");

        builder.Property("courier_id")
               .HasComment("Идентификатор курьера");

        builder.HasOne(p => p.Sender)
               .WithMany()
               .HasForeignKey("sender_id");

        builder.Property("sender_id")
               .HasComment("Идентификатор связанной цели");

        builder.HasOne(p => p.Receiver)
               .WithMany()
               .HasForeignKey("receiver_id");

        builder.Property("receiver_id")
               .HasComment("Идентификатор получателя");

        builder.HasOne(p => p.PaymentMethod)
               .WithMany()
               .HasForeignKey("payment_method_id");

        builder.Property("payment_method_id")
               .HasComment("Идентификатор метода оплаты");

        builder.HasOne(p => p.PackageInformation)
               .WithMany()
               .HasForeignKey("package_information_id");

        builder.Property("package_information_id")
               .HasComment("Идентификатор посылки");

        builder.HasOne(p => p.OrderStatus)
               .WithMany()
               .HasForeignKey("order_status_id");

        builder.Property("order_status_id")
               .HasComment("Идентификатор статуса заказа");
    }
}
