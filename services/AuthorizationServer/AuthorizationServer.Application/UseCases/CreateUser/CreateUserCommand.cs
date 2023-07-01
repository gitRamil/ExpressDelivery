using AuthorizationServer.Application.Dtos;

namespace AuthorizationServer.Application.UseCases.CreateUser;
/// <summary>
/// Представляет команду на создание пользователя.
/// </summary>
/// <param name="UserName">Имя пользователя.</param>
public record CreateUserCommand(string UserName) : IRequest<UserDto>;
