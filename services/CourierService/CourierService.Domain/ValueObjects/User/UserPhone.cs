using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using Domain.Core;

namespace CourierService.Domain.ValueObjects.User;

/// <summary>
/// Представляет телефон сотрудника.
/// </summary>
/// <seealso cref="ValueObject" />
[DebuggerDisplay("{" + nameof(_phone) + "}")]
public sealed partial class UserPhone : ValueObject
{
    private const string Pattern = "^\\+[7]{1}-[0-9]{3}-[0-9]{3}-[0-9]{4}$";
    private readonly string _phone;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="UserPhone" />.
    /// </summary>
    /// <param name="phone">Телефон.</param>
    /// <exception cref="ArgumentException">
    /// Возникает, если <paramref name="phone" />
    /// является <c>null</c> или <c>whitespace</c> или не соответствует формату <see cref="Pattern" />.
    /// </exception>
    public UserPhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
        {
            throw new ArgumentException("Телефон не может быть null или пустой строкой.", nameof(phone));
        }

        if (!MyRegex()
                .IsMatch(phone))
        {
            throw new ArgumentException("Телефон должен быть в формате: +7-000-000-0000.", nameof(phone));
        }

        _phone = phone;
    }

    /// <summary>
    /// Возвращает набор компонентов, участвующий в сравнении.
    /// </summary>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return _phone;
    }

    [GeneratedRegex("^\\+[7]{1}-[0-9]{3}-[0-9]{3}-[0-9]{4}$")]
    private static partial Regex MyRegex();

    /// <summary>
    /// Выполняет явное преобразование из <see cref="string" /> в <see cref="UserPhone" />.
    /// </summary>
    /// <param name="obj">Телефон сотрудника.</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static explicit operator UserPhone?(string? obj) => obj is null ? null : new UserPhone(obj);

    /// <summary>
    /// Выполняет неявное преобразование из <see cref="UserPhone" /> в <see cref="string" />.
    /// </summary>
    /// <param name="obj">Телефон сотрудника.</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static implicit operator string?(UserPhone? obj) => obj?._phone;
}
