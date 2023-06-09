using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Domain.Core;

namespace CourierService.Domain.ValueObjects.User;

/// <summary>
/// Представляет фамилию сотрудника.
/// </summary>
[DebuggerDisplay("{_lastName}")]
public sealed class UserLastName : ValueObject
{
    /// <summary>
    /// Представляет максимальную длину фамилии сотрудника.
    /// </summary>
    public const int MaxLength = 250;

    private readonly string _lastName;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="UserLastName" />.
    /// </summary>
    /// <param name="lastName">Фамилия сотрудника.</param>
    public UserLastName(string lastName)
    {
        if (string.IsNullOrWhiteSpace(lastName))
        {
            throw new ArgumentException("Фамилия сотрудника не может быть null или пустой строкой.", nameof(lastName));
        }

        lastName = lastName.Trim();

        if (lastName.Length > MaxLength)
        {
            throw new ArgumentException($"Фамилия сотрудника не может быть больше {MaxLength}.", nameof(lastName));
        }

        _lastName = lastName;
    }

    /// <summary>
    /// Возвращает набор компонентов, участвующий в сравнении.
    /// </summary>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return _lastName;
    }

    /// <summary>
    /// Выполняет явное преобразование из <see cref="string" /> в <see cref="UserLastName" />.
    /// </summary>
    /// <param name="obj">Фамилия сотрудника.</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static explicit operator UserLastName?(string? obj) => obj is null ? null : new UserLastName(obj);

    /// <summary>
    /// Выполняет неявное преобразование из <see cref="UserLastName" /> в <see cref="string" />.
    /// </summary>
    /// <param name="obj">Фамилия сотрудника.</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static implicit operator string?(UserLastName? obj) => obj?._lastName;
}
