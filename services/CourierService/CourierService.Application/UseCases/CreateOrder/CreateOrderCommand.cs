using CourierService.Application.Dtos;

namespace CourierService.Application.UseCases.CreateOrder;

/// <summary>
/// Представляет команду создания заказа.
/// </summary>
/// <param name="SenderName">Имя отправителя.</param>
/// <param name="SenderAddress">Адрес отправителя.</param>
/// <param name="ReceiverName">Имя получателя.</param>
/// <param name="ReceiverAddress">Адрес получателя.</param>
/// <param name="DeliveryCost">Стоимость доставки.</param>
/// <param name="PaymentMethod">Метод оплаты.</param>
/// <param name="ProductCost">Стоимость посылки.</param>
/// <param name="ProductDescription">Описание посылки.</param>
/// <param name="ProductWeight">Вес посылки.</param>
public record CreateOrderCommand(string SenderName,
                                 string SenderAddress,
                                 string ReceiverName,
                                 string ReceiverAddress,
                                 decimal DeliveryCost,
                                 PaymentMethodCodeEnum PaymentMethod,
                                 decimal ProductCost,
                                 string ProductDescription,
                                 int ProductWeight) : IRequest<Guid>
{
}
