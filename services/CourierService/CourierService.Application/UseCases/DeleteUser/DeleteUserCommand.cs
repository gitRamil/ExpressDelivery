namespace CourierService.Application.UseCases.DeleteUser;

/// <summary>
/// Представляет команду для удаления пользователя.
/// </summary>
/// <param name="UserId">Идентификатор.</param>
public record DeleteUserCommand(Guid UserId) : IRequest;
