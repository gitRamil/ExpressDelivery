namespace AuthorizationServer.Application.UseCases.ConfirmUser;

/// <summary>
/// Представляет команду подтверждающую пользователя.
/// </summary>
/// <param name="UserId">Идентификатор пользователя.</param>
/// <param name="UserPhone">Телефон пользователя.</param>
public record ConfirmUserCommand(Guid UserId, string UserPhone) : IRequest;
