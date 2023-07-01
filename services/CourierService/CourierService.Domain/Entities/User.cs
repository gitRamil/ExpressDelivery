using CourierService.Domain.Core;
using CourierService.Domain.Entities.Dictionaries;
using CourierService.Domain.ValueObjects.User;
using Domain.Core.Primitives;

namespace CourierService.Domain.Entities;

/// <summary>
/// Представляет сущность пользователя.
/// </summary>
public class User : TrackedEntity<SequentialGuid>
{
    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="User" />.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="login">Логин.</param>
    /// <param name="mail">Эл. почта.</param>
    /// <param name="firstName">Имя.</param>
    /// <param name="passwordHash">Хеш пароля.</param>
    /// <param name="passwordSalt">Соль пароля.</param>
    /// <param name="right">Права.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <see cref="login" />
    /// или <see cref="firstName" />
    /// или <see cref="mail" />
    /// или <see cref="firstName" />
    /// или <see cref="passwordHash" />
    /// или <see cref="passwordSalt" />
    /// или <see cref="right" /> равно <c>null</c>.
    /// </exception>
    public User(SequentialGuid id, UserLogin login, UserMail mail, UserFirstName firstName, byte[] passwordHash, byte[] passwordSalt, Right right)
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
    public UserFirstName FirstName { get; set; }

    /// <summary>
    /// Возвращает фамилию.
    /// </summary>
    public UserLastName? LastName { get; set; }

    /// <summary>
    /// Возвращает логин пользователя.
    /// </summary>
    public UserLogin Login { get; set; }

    /// <summary>
    /// Возвращает эл. почту.
    /// </summary>
    public UserMail Mail { get; set; }

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
    public UserPhone? Phone { get; set; }

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
