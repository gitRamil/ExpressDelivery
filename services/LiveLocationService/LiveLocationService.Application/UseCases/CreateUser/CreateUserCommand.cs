using LiveLocationService.Application.Dtos;

namespace LiveLocationService.Application.UseCases.CreateUser;
/// <summary>
/// Представляет команду на создание пользователя.
/// </summary>
/// <param name="UserName">Имя пользователя.</param>
public record CreateUserCommand(string UserName) : IRequest<UserDto>;
