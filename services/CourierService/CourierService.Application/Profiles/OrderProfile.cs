using AutoMapper;
using CourierService.Application.Dtos;
using CourierService.Application.Extensions;
using CourierService.Domain.Entities;
using CourierService.Domain.ValueObjects.Dictionaries.OrderStatus;
using CourierService.Domain.ValueObjects.Dictionaries.PaymentMethod;

namespace CourierService.Application.Profiles;

/// <summary>
/// Представляет конфигурацию преобразования для типа <see cref="Order" />.
/// </summary>
public class OrderProfile : Profile
{
    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="OrderProfile" />.
    /// </summary>
    public OrderProfile()
    {
        CreateMap<Order, OrderDto>()
            .ForMember(dto => dto.PaymentMethod, expression => expression.MapFrom(order => order.PaymentMethod.Code))
            .ForMember(dto => dto.OrderStatus, expression => expression.MapFrom(order => order.OrderStatus.Code));

        CreateMap<PaymentMethodCode, PaymentMethodCodeEnum>()
            .ConvertUsing(c => c.ToPaymentMethod());

        CreateMap<OrderStatusCode, OrderStatusCodeEnum>()
            .ConvertUsing(c => c.ToOrderStatus());
    }
}
