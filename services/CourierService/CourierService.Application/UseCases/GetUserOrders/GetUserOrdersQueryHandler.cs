using AutoMapper;
using CourierService.Application.Abstractions;
using CourierService.Application.Dtos;
using CourierService.Application.Exceptions;
using CourierService.Application.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CourierService.Application.UseCases.GetUserOrders;

/// <summary>
/// Представляет обработчик запроса на получение заказов пользователя.
/// </summary>
public class GetUserOrdersQueryHandler : IRequestHandler<GetUserOrdersQuery, IEnumerable<OrderDto>>
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMapper _mapper;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="GetUserOrdersQueryHandler" />.
    /// </summary>
    /// <param name="context">Контекст БД.</param>
    /// <param name="mapper">Маппер.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="context" /> равен <c>null</c>.
    /// </exception>
    public GetUserOrdersQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _appDbContext = context ?? throw new ArgumentNullException(nameof(context));
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
    public async Task<IEnumerable<OrderDto>> Handle(GetUserOrdersQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var query = _appDbContext.Orders.Where(order => true);

        query = query.OrderBy(order => order.CreatedDate)
                     .ApplyPagination(request.Skip, request.Take);
        var orders = await query.ToListAsync(cancellationToken);

        return orders.Select(order => _mapper.Map<OrderDto>(order));
    }
}
