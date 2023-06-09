using Domain.Core;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace CourierService.Domain.ValueObjects;
/// <summary>
/// Представляет телефон пользователя.
/// </summary>
/// <seealso cref="ValueObject" />
[DebuggerDisplay("{" + nameof(_phone) + "}")]
public sealed class UserPhone : ValueObject
{
    private const string Pattern = "^\\+[7]{1}-[0-9]{3}-[0-9]{3}-[0-9]{4}$";
    private readonly string _phone;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="UserPhone" />.
    /// </summary>
    /// <param name="phone">Телефон.</param>
    /// <exception cref="System.ArgumentException">
    /// Возникает, если <paramref name="phone" />
    /// является <c>null</c> или <c>whitespace</c> или не соответствует формату <see cref="Pattern" />.
    /// </exception>
    public UserPhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
        {
            throw new ArgumentException("Телефон не может быть null или пустой строкой.", nameof(phone));
        }

        if (!Regex.IsMatch(phone, Pattern))
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

    /// <summary>
    /// Выполняет явное преобразование из <see cref="UserPhone" /> в <see cref="string" />.
    /// </summary>
    /// <param name="obj">Телефон.</param>
    /// <returns>
    /// Результат преобразования.
    /// </returns>
    public static explicit operator UserPhone?(string? obj)
    {
        if (obj == null)
        {
            return null;
        }

        return new UserPhone(obj);
    }

    /// <summary>
    /// Выполняет неявное преобразование из <see cref="UserPhone" /> в <see cref="string" />.
    /// </summary>
    /// <param name="obj">Телефон.</param>
    /// <returns>
    /// Результат преобразования.
    /// </returns>
    public static implicit operator string?(UserPhone? obj) => obj?._phone;
}
