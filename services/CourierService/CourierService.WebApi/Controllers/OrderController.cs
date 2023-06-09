using CourierService.Application.UseCases.CreateOrder;
using MediatR;

namespace CourierService.WebApi.Controllers;

/// <summary>
/// Представляет конечную точку по взаимодействию с заказами.
/// </summary>
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiExplorerSettings(GroupName = "v1")]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="OrderController" />.
    /// </summary>
    /// <param name="mediator">Медиатор.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="mediator" /> равен <c>null</c>.
    /// </exception>
    public OrderController(IMediator mediator) => _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    /// <summary>
    /// Создает новый заказ.
    /// </summary>
    /// <param name="command">Команда создания периода.</param>
    /// <param name="cancellationToken">Маркер отмены.</param>
    [HttpPost]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }
}
