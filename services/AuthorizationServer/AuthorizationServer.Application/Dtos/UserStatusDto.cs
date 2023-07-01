namespace AuthorizationServer.Application.Dtos;

/// <summary>
/// Содержит набор значений, представляющих статусы пользователя.
/// </summary>
public enum UserStatusDto
{
    /// <summary>
    /// Неподтвержденный.
    /// </summary>
    Unverified = 0,

    /// <summary>
    /// Подтвержденный.
    /// </summary>
    Verified = 1
}
