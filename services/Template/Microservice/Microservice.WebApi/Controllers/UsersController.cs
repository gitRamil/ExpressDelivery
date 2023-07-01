using Microservice.Application.Dtos;
using Microservice.Application.UseCases.ConfirmUser;
using Microservice.Application.UseCases.CreateUser;
using Microservice.Application.UseCases.DeleteUser;
using Microservice.Application.UseCases.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace Microservice.WebApi.Controllers;

/// <summary>
/// Представляет конечную точку по взаимодействию с пользователями.
/// </summary>
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiExplorerSettings(GroupName = "v1")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="UsersController" />.
    /// </summary>
    /// <param name="mediator">Медиатор.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="mediator" /> равен <c>null</c>.
    /// </exception>
    public UsersController(IMediator mediator) => _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    /// <summary>
    /// Подтверждает пользователя.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <param name="userPhone">Телефон пользователя.</param>
    /// <param name="token">Маркер отмены.</param>
    [HttpPut("{id:guid}/confirmation")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ConfirmUser(Guid id, [FromBody] string userPhone, CancellationToken token)
    {
        await _mediator.Send(new ConfirmUserCommand(id, userPhone), token);
        return NoContent();
    }

    /// <summary>
    /// Создает пользователя.
    /// </summary>
    /// <param name="command">Команда, создающая пользователя.</param>
    /// <param name="token">Маркер отмены.</param>
    /// <returns>Новый созданный пользователь.</returns>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command, CancellationToken token)
    {
        var user = await _mediator.Send(command, token);

        return CreatedAtAction(nameof(GetUserById),
                               new
                               {
                                   id = user.Id
                               },
                               user);
    }

    /// <summary>
    /// Удаляет пользователя по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="token">Маркер отмены.</param>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUser(Guid id, CancellationToken token)
    {
        await _mediator.Send(new DeleteUserCommand(id), token);
        return NoContent();
    }

    /// <summary>
    /// Возвращает пользователя по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="token">Маркер отмены.</param>
    /// <returns>Пользователь.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserById(Guid id, CancellationToken token)
    {
        var user = await _mediator.Send(new GetUserByIdQuery(id), token);
        return Ok(user);
    }
}
