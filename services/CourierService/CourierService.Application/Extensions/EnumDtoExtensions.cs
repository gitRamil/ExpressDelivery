using CourierService.Application.Dtos;
using CourierService.Domain.Entities.Dictionaries;

namespace CourierService.Application.Extensions;


/// <summary>
/// Содержит набор методов расширения для работы c перечислениями.
/// </summary>
public static class EnumDtoExtensions
{
    /// <summary>
    /// Возвращает метод оплаты на основе значения перечисления.
    /// </summary>
    /// <param name="code">Код метода оплаты.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Возникает, если значение перечисления не поддерживается.
    /// </exception>
    public static PaymentMethod ToPaymentMethod(this PaymentMethodCode code) =>
        code switch
        {
            PaymentMethodCode.Card => PaymentMethod.Card,
            PaymentMethodCode.Cash => PaymentMethod.Cash,
            PaymentMethodCode.Online => PaymentMethod.Online,
            _ => throw new ArgumentOutOfRangeException(nameof(code), code, "Не поддерживаемое значение метода оплаты.")
        };
}
