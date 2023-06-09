using System.Runtime.Serialization;

namespace CourierService.Application.Dtos;

/// <summary>
/// Представляет набор значений, описывающих метод оплаты.
/// </summary>
public enum PaymentMethodCode
{
    /// <summary>
    /// Карта.
    /// </summary>
    [EnumMember(Value = "card")]
    Card,

    /// <summary>
    /// Наличные.
    /// </summary>
    [EnumMember(Value = "cash")]
    Cash,

    /// <summary>
    /// Онлайн.
    /// </summary>
    [EnumMember(Value = "online")]
    Online
}
