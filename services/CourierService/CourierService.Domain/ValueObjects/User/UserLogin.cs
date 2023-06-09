using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Domain.Core;

namespace CourierService.Domain.ValueObjects.User;

/// <summary>
/// Представляет логин сотрудника.
/// </summary>
[DebuggerDisplay("{_login}")]
public sealed class UserLogin : ValueObject
{
    /// <summary>
    /// Представляет максимальную длину логина сотрудника.
    /// </summary>
    public const int MaxLength = 250;

    private readonly string _login;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="UserLogin" />.
    /// </summary>
    /// <param name="login">Логин сотрудника.</param>
    public UserLogin(string login)
    {
        if (string.IsNullOrWhiteSpace(login))
        {
            throw new ArgumentException("Логин сотрудника не может быть null или пустой строкой.", nameof(login));
        }
        
        login = login.Trim();

        if (login.Length > MaxLength)
        {
            throw new ArgumentException($"Логин сотрудника не может быть больше {MaxLength}.", nameof(login));
        }

        _login = login;
    }

    /// <summary>
    /// Возвращает набор компонентов, участвующий в сравнении.
    /// </summary>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return _login;
    }

    /// <summary>
    /// Выполняет явное преобразование из <see cref="string" /> в <see cref="UserLogin" />.
    /// </summary>
    /// <param name="obj">Логин сотрудника.</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static explicit operator UserLogin?(string? obj) => obj is null ? null : new UserLogin(obj);

    /// <summary>
    /// Выполняет неявное преобразование из <see cref="UserLogin" /> в <see cref="string" />.
    /// </summary>
    /// <param name="obj">Логин сотрудника.</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static implicit operator string?(UserLogin? obj) => obj?._login;
}
