using CourierService.Domain.Entities.Dictionaries;
using CourierService.Domain.ValueObjects.Order;
using Domain.Core;
using Domain.Core.Primitives;

namespace CourierService.Domain.Entities;

/// <summary>
/// Представляет сущность заказа.
/// </summary>
public class Order : Entity<SequentialGuid>
{
    public Order(SequentialGuid id,
                 // User sender,
                 OrderSenderName senderName,
                 OrderSenderAddress senderAddress,
                 OrderReceiverName receiverName,
                 OrderReceiverAddress receiverAddress,
                 OrderDeliveryCost deliveryCost,
                 PaymentMethod paymentMethod,
                 PackageInformation packageInformation,
                 OrderStatus orderStatus)
        : base(id)
    {
        // Sender = sender ?? throw new ArgumentNullException(nameof(sender));
        SenderName = senderName ?? throw new ArgumentNullException(nameof(senderName));
        SenderAddress = senderAddress ?? throw new ArgumentNullException(nameof(senderAddress));
        ReceiverName = receiverName ?? throw new ArgumentNullException(nameof(receiverName));
        ReceiverAddress = receiverAddress ?? throw new ArgumentNullException(nameof(receiverAddress));
        DeliveryCost = deliveryCost;
        PaymentMethod = paymentMethod ?? throw new ArgumentNullException(nameof(paymentMethod));
        PackageInformation = packageInformation ?? throw new ArgumentNullException(nameof(packageInformation));
        OrderStatus = orderStatus ?? throw new ArgumentNullException(nameof(orderStatus));
    }

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="Order" />.
    /// </summary>
    /// <remarks>Конструктор для EF.</remarks>
    protected Order()
        : base(SequentialGuid.Empty)
    {
        // Sender = null!;
        SenderName = null!;
        SenderAddress = null!;
        ReceiverName = null!;
        ReceiverAddress = null!;
        PaymentMethod = null!;
        PackageInformation = null!;
        OrderStatus = null!;
    }

    /// <summary>
    /// Возвращает информацию о курьере.
    /// </summary>
    public virtual Courier? Courier { get; set; }

    /// <summary>
    /// Возвращает цену доставки.
    /// </summary>
    public OrderDeliveryCost? DeliveryCost { get; }

    /// <summary>
    /// Возвращает информацию о дате доставки.
    /// </summary>
    public OrderDeliveryDate? DeliveryDate { get; set; }

    /// <summary>
    /// Возвращает информацию о статусе заказа.
    /// </summary>
    public virtual OrderStatus OrderStatus { get; set; }

    /// <summary>
    /// Возвращает информацию о посылке.
    /// </summary>
    public virtual PackageInformation PackageInformation { get; }

    /// <summary>
    /// Возвращает метод оплаты заказа.
    /// </summary>
    public virtual PaymentMethod PaymentMethod { get; }

    /// <summary>
    /// Возвращает получателя.
    /// </summary>
    public virtual User? Receiver { get; set; }

    /// <summary>
    /// Возвращает адрес получателя.
    /// </summary>
    public OrderReceiverAddress ReceiverAddress { get; }

    /// <summary>
    /// Возвращает имя получателя.
    /// </summary>
    public OrderReceiverName ReceiverName { get; }

    /// <summary>
    /// Возвращает отправителя.
    /// </summary>
    public virtual User? Sender { get; }

    /// <summary>
    /// Возвращает адрес отправителя.
    /// </summary>
    public OrderSenderAddress SenderAddress { get; }

    /// <summary>
    /// Возвращает имя отправителя.
    /// </summary>
    public OrderSenderName SenderName { get; }

    /// <summary>
    /// Возвращает трек номер.
    /// </summary>
    public SequentialGuid TrackNumber { get; } = SequentialGuid.Create();
}
