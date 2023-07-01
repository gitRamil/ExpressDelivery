using LiveLocationService.Application.Dtos;

namespace LiveLocationService.Application.UseCases.GetUserById;
/// <summary>
/// Представляет запрос для получения пользователя.
/// </summary>
/// <param name="UserId">Идентификатор пользователя.</param>
public record GetUserByIdQuery(Guid UserId) : IRequest<UserDto>;
