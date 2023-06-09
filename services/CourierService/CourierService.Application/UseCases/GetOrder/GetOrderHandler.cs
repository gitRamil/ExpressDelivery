using AutoMapper;
using CourierService.Application.Abstractions;
using CourierService.Application.Dtos;
using CourierService.Application.Exceptions;
using CourierService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourierService.Application.UseCases.GetOrder;

/// <summary>
/// Представляет обработчик запроса на получение заказа.
/// </summary>
public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, OrderDto>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="GetOrderQueryHandler" />.
    /// </summary>
    /// <param name="context">Контекст БД.</param>
    /// <param name="mapper">Маппер.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="context" /> равен <c>null</c>.
    /// </exception>
    public GetOrderQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    /// <summary>Выполняет команду.</summary>
    /// <param name="request">Команда.</param>
    /// <param name="cancellationToken">Маркер отмены.</param>
    /// <exception cref="NotFoundException">
    /// Возникает, если период не найден.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="request" /> равен <c>null</c>.
    /// </exception>
    public async Task<OrderDto> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var order = await _context.Orders.FirstOrDefaultAsync(order => (Guid)order.TrackNumber == request.TrackNumber, cancellationToken) ??
                    throw new NotFoundException(typeof(Order), request.TrackNumber);
        return _mapper.Map<OrderDto>(order);
    }
}
