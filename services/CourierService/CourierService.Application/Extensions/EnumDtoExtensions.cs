using CourierService.Application.Dtos;
using CourierService.Domain.Entities.Dictionaries;
using CourierService.Domain.ValueObjects.Dictionaries.OrderStatus;
using CourierService.Domain.ValueObjects.Dictionaries.PaymentMethod;

namespace CourierService.Application.Extensions;

/// <summary>
/// Содержит набор методов расширения для работы c перечислениями.
/// </summary>
public static class EnumDtoExtensions
{
    /// <summary>
    /// Возвращает значения перечисления статуса заказа на основе кода статуса заказа.
    /// </summary>
    /// <param name="orderStatusCode">Код статуса заказа.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Возникает, если код статса заказа не поддерживается.
    /// </exception>
    public static OrderStatusCodeEnum ToOrderStatus(this OrderStatusCode orderStatusCode) =>
        orderStatusCode switch
        {
            not null when orderStatusCode == OrderStatus.CourierAssigned.Code => OrderStatusCodeEnum.CourierAssigned,
            not null when orderStatusCode == OrderStatus.Created.Code => OrderStatusCodeEnum.Created,
            not null when orderStatusCode == OrderStatus.Done.Code => OrderStatusCodeEnum.Done,
            not null when orderStatusCode == OrderStatus.InProgress.Code => OrderStatusCodeEnum.InProgress,
            _ => throw new ArgumentOutOfRangeException(nameof(orderStatusCode), orderStatusCode, "Не поддерживаемое значение статуса заказа.")
        };

    /// <summary>
    /// Возвращает статус заказа на основе значения перечисления.
    /// </summary>
    /// <param name="codeEnum">Код статуса заказа.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Возникает, если значение перечисления не поддерживается.
    /// </exception>
    public static OrderStatus ToOrderStatus(this OrderStatusCodeEnum codeEnum) =>
        codeEnum switch
        {
            OrderStatusCodeEnum.CourierAssigned => OrderStatus.CourierAssigned,
            OrderStatusCodeEnum.Created => OrderStatus.Created,
            OrderStatusCodeEnum.Done => OrderStatus.Done,
            OrderStatusCodeEnum.InProgress => OrderStatus.InProgress,
            _ => throw new ArgumentOutOfRangeException(nameof(codeEnum), codeEnum, "Не поддерживаемое значение статуса заказа.")
        };

    /// <summary>
    /// Возвращает значения перечисления метода оплаты на основе кода метода оплаты.
    /// </summary>
    /// <param name="pymentMethodCode">Код метода оплаты.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Возникает, если код метода оплаты не поддерживается.
    /// </exception>
    public static PaymentMethodCodeEnum ToPaymentMethod(this PaymentMethodCode pymentMethodCode) =>
        pymentMethodCode switch
        {
            not null when pymentMethodCode == PaymentMethod.Card.Code => PaymentMethodCodeEnum.Card,
            not null when pymentMethodCode == PaymentMethod.Cash.Code => PaymentMethodCodeEnum.Cash,
            not null when pymentMethodCode == PaymentMethod.Online.Code => PaymentMethodCodeEnum.Online,
            _ => throw new ArgumentOutOfRangeException(nameof(pymentMethodCode), pymentMethodCode, "Не поддерживаемое значение метода оплаты.")
        };

    /// <summary>
    /// Возвращает метод оплаты на основе значения перечисления.
    /// </summary>
    /// <param name="codeEnum">Код метода оплаты.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Возникает, если значение перечисления не поддерживается.
    /// </exception>
    public static PaymentMethod ToPaymentMethod(this PaymentMethodCodeEnum codeEnum) =>
        codeEnum switch
        {
            PaymentMethodCodeEnum.Card => PaymentMethod.Card,
            PaymentMethodCodeEnum.Cash => PaymentMethod.Cash,
            PaymentMethodCodeEnum.Online => PaymentMethod.Online,
            _ => throw new ArgumentOutOfRangeException(nameof(codeEnum), codeEnum, "Не поддерживаемое значение метода оплаты.")
        };
}
