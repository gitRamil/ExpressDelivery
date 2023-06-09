using CourierService.Domain.Entities.Dictionaries;
using Domain.Core;
using Domain.Core.Primitives;

namespace CourierService.Domain.Entities;

/// <summary>
/// Представляет сущность пользователя.
/// </summary>
public class User : Entity<SequentialGuid>
{
    public User(SequentialGuid id, string login, string mail, string firstName, byte[] passwordHash, byte[] passwordSalt, Right right)
        : base(id)
    {
        Login = login ?? throw new ArgumentNullException(nameof(login));
        Mail = mail ?? throw new ArgumentNullException(nameof(mail));
        FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
        PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
        PasswordSalt = passwordSalt ?? throw new ArgumentNullException(nameof(passwordSalt));
        Right = right ?? throw new ArgumentNullException(nameof(right));
    }

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="User" />.
    /// </summary>
    /// <remarks>Конструктор для EF.</remarks>
    protected User()
        : base(SequentialGuid.Empty)
    {
        Login = null!;
        Mail = null!;
        FirstName = null!;
        PasswordHash = null!;
        PasswordSalt = null!;
        Right = null!;
    }

    /// <summary>
    /// Возвращает имя.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Возвращает фамилию.
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Возвращает логин пользователя.
    /// </summary>
    public string Login { get; set; }

    /// <summary>
    /// Возвращает эл. почту.
    /// </summary>
    public string Mail { get; set; }

    /// <summary>
    /// Возвращает хеш пароля.
    /// </summary>
    public byte[] PasswordHash { get; set; }

    /// <summary>
    /// Возвращает соль пароля.
    /// </summary>
    public byte[] PasswordSalt { get; set; }

    /// <summary>
    /// Возвращает номер телефона.
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// Возвращает рефреш-токен.
    /// </summary>
    public string RefreshToken { get; set; } = string.Empty;

    /// <summary>
    /// Возвращает права пользователя.
    /// </summary>
    public virtual Right Right { get; }

    /// <summary>
    /// Возвращает дату создания токена.
    /// </summary>
    public DateTime TokenCreated { get; set; }

    /// <summary>
    /// Возвращает дату истечения токена.
    /// </summary>
    public DateTime TokenExpires { get; set; }
}
