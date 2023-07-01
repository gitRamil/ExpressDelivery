using CourierService.Application.Dtos;
using CourierService.Domain.ValueObjects.Dictionaries.OrderStatus;

namespace CourierService.Application.UseCases.GetUserOrders;

/// <summary>
/// Представляет запрос на получение заказов пользователя.
/// </summary>
public record GetUserOrdersQuery : IRequest<IEnumerable<OrderDto>>
{
    /// <summary>
    /// Возвращает код статуса заказа.
    /// </summary>
    public OrderStatusCode? OrderStatusCode { get; init; }

    /// <summary>
    /// Возвращает количество пропускаемы сущностей.
    /// </summary>
    public int? Skip { get; init; } = 0;

    /// <summary>
    /// Возвращает количество запрашиваемых сущностей.
    /// </summary>
    public int? Take { get; init; } = 10;
}
