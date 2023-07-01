namespace Microservice.Application.Abstractions;

/// <summary>
/// Описывает текущего пользователя.
/// </summary>
public interface ICurrentUser
{
    /// <summary>
    /// Возвращает идентификатор текущего пользователя.
    /// </summary>
    /// <returns>
    /// Идентификатор текущего пользователя или <see cref="Guid.Empty" />, если пользователь является неаутентифицированным.
    /// </returns>
    Guid Id { get; }

    /// <summary>
    /// Возвращает признак, определяющий является ли текущий пользователь аутентифицированным.
    /// </summary>
    /// <returns>
    /// <c>true</c> если пользователь является аутентифицированным; в противном случае, <c>false</c>.
    /// </returns>
    bool IsAuthenticated { get; }
}
