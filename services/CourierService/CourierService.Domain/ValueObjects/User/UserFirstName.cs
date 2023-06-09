using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Domain.Core;

namespace CourierService.Domain.ValueObjects.User;

/// <summary>
/// Представляет имя сотрудника.
/// </summary>
[DebuggerDisplay("{_firstName}")]
public sealed class UserFirstName : ValueObject
{
    /// <summary>
    /// Представляет максимальную длину имени сотрудника.
    /// </summary>
    public const int MaxLength = 250;

    private readonly string _firstName;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="UserFirstName" />.
    /// </summary>
    /// <param name="firstName">Имя сотрудника.</param>
    public UserFirstName(string firstName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw new ArgumentException("Имя сотрудника не может быть null или пустой строкой.", nameof(firstName));
        }

        firstName = firstName.Trim();

        if (firstName.Length > MaxLength)
        {
            throw new ArgumentException($"Имя сотрудника не может быть больше {MaxLength}.", nameof(firstName));
        }

        _firstName = firstName;
    }

    /// <summary>
    /// Возвращает набор компонентов, участвующий в сравнении.
    /// </summary>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return _firstName;
    }

    /// <summary>
    /// Выполняет явное преобразование из <see cref="string" /> в <see cref="UserFirstName" />.
    /// </summary>
    /// <param name="obj">Имя сотрудника.</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static explicit operator UserFirstName?(string? obj) => obj is null ? null : new UserFirstName(obj);

    /// <summary>
    /// Выполняет неявное преобразование из <see cref="UserFirstName" /> в <see cref="string" />.
    /// </summary>
    /// <param name="obj">Имя сотрудника.</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static implicit operator string?(UserFirstName? obj) => obj?._firstName;
}
