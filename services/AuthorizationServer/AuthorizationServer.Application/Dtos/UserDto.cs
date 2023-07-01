namespace AuthorizationServer.Application.Dtos;

/// <summary>
/// Представляет пользователя.
/// </summary>
public record UserDto
{
    /// <summary>
    /// Возвращает или устанавливает идентификатор.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Возвращает или устанавливает имя.
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Возвращает или устанавливает телефон.
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// Возвращает или устанавливает идентификатор.
    /// </summary>
    public UserStatusDto Status { get; set; }
}
