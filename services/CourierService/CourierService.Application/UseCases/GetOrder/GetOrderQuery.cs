using CourierService.Application.Dtos;

namespace CourierService.Application.UseCases.GetOrder;

/// <summary>
/// Представляет запрос на получение заказа.
/// </summary>
/// <param name="TrackNumber">Трек номер заказа.</param>
public record GetOrderQuery(Guid TrackNumber) : IRequest<OrderDto>;
