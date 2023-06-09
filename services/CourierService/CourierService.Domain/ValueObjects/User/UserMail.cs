using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Domain.Core;

namespace CourierService.Domain.ValueObjects.User;

/// <summary>
/// Представляет почту сотрудника.
/// </summary>
[DebuggerDisplay("{_mail}")]
public sealed class UserMail : ValueObject
{
    /// <summary>
    /// Представляет максимальную длину почты сотрудника.
    /// </summary>
    public const int MaxLength = 250;

    private readonly string _mail;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="UserMail" />.
    /// </summary>
    /// <param name="mail">Почта сотрудника.</param>
    public UserMail(string mail)
    {
        if (string.IsNullOrWhiteSpace(mail))
        {
            throw new ArgumentException("Почта сотрудника не может быть null или пустой строкой.", nameof(mail));
        }

        if (mail.Length > MaxLength)
        {
            throw new ArgumentException($"Почта сотрудника не может быть больше {MaxLength}.", nameof(mail));
        }

        _mail = mail;
    }

    /// <summary>
    /// Возвращает набор компонентов, участвующий в сравнении.
    /// </summary>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return _mail;
    }

    /// <summary>
    /// Выполняет явное преобразование из <see cref="string" /> в <see cref="UserMail" />.
    /// </summary>
    /// <param name="obj">Почта сотрудника.</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static explicit operator UserMail?(string? obj) => obj is null ? null : new UserMail(obj);

    /// <summary>
    /// Выполняет неявное преобразование из <see cref="UserMail" /> в <see cref="string" />.
    /// </summary>
    /// <param name="obj">Почта сотрудника.</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static implicit operator string?(UserMail? obj) => obj?._mail;
}
