using CourierService.Domain.Enums;
using CourierService.Domain.ValueObjects;
using Domain.Core;
using Domain.Core.Primitives;

namespace CourierService.Domain.Entities;
/// <summary>
/// Представляет пользователя.
/// </summary>
public class User : Entity<SequentialGuid>
{
    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="User" />.
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="name">Имя.</param>
    /// <exception cref="System.ArgumentNullException">Возникает, если <see cref="name" /> равно <c>null</c></exception>
    public User(SequentialGuid id, UserName name)
        : base(id)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Status = UserStatus.Unverified;
    }

    /// <summary>
    /// Возвращает имя.
    /// </summary>
    public UserName Name { get; }

    /// <summary>
    /// Возвращает телефон.
    /// </summary>
    public UserPhone? Phone { get; private set; }

    /// <summary>
    /// Возвращает статус.
    /// </summary>
    public UserStatus Status { get; private set; }

    /// <summary>
    /// Подтверждает пользователя.
    /// </summary>
    /// <param name="phone">Телефон.</param>
    /// <exception cref="ArgumentNullException">Возникает, если <see cref="phone" /> равен <c>null</c></exception>
    public void Confirm(UserPhone phone)
    {
        Phone = phone ?? throw new ArgumentNullException(nameof(phone));
        Status = UserStatus.Verified;
    }
}
