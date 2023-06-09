using System.Runtime.Serialization;

namespace CourierService.Application.Dtos;

/// <summary>
/// Представляет набор значений, описывающих статусы заказа.
/// </summary>
public enum OrderStatusCodeEnum
{
    /// <summary>
    /// Курьер назначен.
    /// </summary>
    [EnumMember(Value = "courier_assigned")]
    CourierAssigned,

    /// <summary>
    /// Создан.
    /// </summary>
    [EnumMember(Value = "created")]
    Created,

    /// <summary>
    /// Выполнен.
    /// </summary>
    [EnumMember(Value = "done")]
    Done,

    /// <summary>
    /// Выполняется.
    /// </summary>
    [EnumMember(Value = "in_progress")]
    InProgress
}
