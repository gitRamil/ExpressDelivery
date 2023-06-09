using CourierService.Domain.Entities;
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

        builder.HasOne(p => p.Courier)
               .WithMany()
               .HasForeignKey("courier_id");

        builder.Property("courier_id")
               .HasComment("Идентификатор курьера");

        builder.Property(p => p.DeliveryCost)
               .HasComment("Цена доставки");

        builder.Property(p => p.DeliveryDate)
               .HasComment("Дата доставки");

        builder.HasOne(p => p.OrderStatus)
               .WithMany()
               .HasForeignKey("order_status_id");

        builder.Property("order_status_id")
               .HasComment("Идентификатор статуса заказа");

        builder.HasOne(p => p.PackageInformation)
               .WithMany()
               .HasForeignKey("package_information_id");

        builder.Property("package_information_id")
               .HasComment("Идентификатор посылки");

        builder.HasOne(p => p.PaymentMethod)
               .WithMany()
               .HasForeignKey("payment_method_id");

        builder.Property("payment_method_id")
               .HasComment("Идентификатор метода оплаты");

        builder.HasOne(p => p.Receiver)
               .WithMany()
               .HasForeignKey("receiver_id");

        builder.Property("receiver_id")
               .HasComment("Идентификатор получателя");

        builder.Property(p => p.ReceiverAddress)
               .HasComment("Адрес получателя");

        builder.Property(p => p.ReceiverName)
               .HasComment("Имя получателя");

        builder.HasOne(p => p.Sender)
               .WithMany()
               .HasForeignKey("sender_id");

        builder.Property("sender_id")
               .HasComment("Идентификатор связанной цели");

        builder.Property(p => p.SenderAddress)
               .HasComment("Адрес отправителя");

        builder.Property(p => p.SenderName)
               .HasComment("Имя отправителя");

        builder.Property(p => p.TrackNumber)
               .HasComment("Номер отслеживания");
    }
}
