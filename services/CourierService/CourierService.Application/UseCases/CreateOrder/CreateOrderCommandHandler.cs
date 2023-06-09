using CourierService.Application.Abstractions;
using CourierService.Domain.Entities;
using CourierService.Domain.Entities.Dictionaries;
using CourierService.Domain.ValueObjects.Dictionaries.PaymentMethod;
using CourierService.Domain.ValueObjects.Order;
using CourierService.Domain.ValueObjects.PackageInformation;
using Domain.Core.Primitives;

namespace CourierService.Application.UseCases.CreateOrder;

/// <summary>
/// Представляет обработчик команды создания цели.
/// </summary>
public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, string>
{
    private readonly IAppDbContext _context;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="CreateOrderCommandHandler" />.
    /// </summary>
    /// <param name="context">Контекст БД.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="context" /> равен <c>null</c>.
    /// </exception>
    public CreateOrderCommandHandler(IAppDbContext context) => _context = context ?? throw new ArgumentNullException(nameof(context));

    /// <summary>Выполняет команду.</summary>
    /// <param name="request">Команда.</param>
    /// <param name="cancellationToken">Маркер отмены.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="request" /> равен <c>null</c>.
    /// </exception>
    public async Task<string> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var paymentMethod = PaymentMethod.FromValue(new PaymentMethodCode(request.PaymentMethod));

        var packageInformation = new PackageInformation(SequentialGuid.Create(),
                                                        (PackageInformationShortDescription)request.ProductDescription,
                                                        new PackageInformationWeight(request.ProductWeight),
                                                        new PackageInformationCost(request.ProductCost));

        var order = new Order(SequentialGuid.Create(),
                              (OrderSenderName)request.SenderName,
                              (OrderSenderAddress)request.SenderAddress,
                              (OrderReceiverName)request.ReceiverName,
                              (OrderReceiverAddress)request.ReceiverAddress,
                              new OrderDeliveryCost(request.DeliveryCost),
                              paymentMethod,
                              packageInformation,
                              OrderStatus.Created);

        _context.Orders.Add(order);

        await _context.SaveChangesAsync(cancellationToken);

        return order.TrackNumber;
    }
}
