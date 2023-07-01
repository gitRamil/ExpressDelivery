using Domain.Core;

namespace Microservice.Domain.ValueObjects;

/// <summary>
/// Представляет имя пользователя.
/// </summary>
public sealed class UserName : ValueObject
{
    private const int MaxLength = 100;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="UserName" />.
    /// </summary>
    /// <param name="name">Наименование.</param>
    /// <exception cref="System.ArgumentException">
    /// Возникает, если <paramref name="name" />
    /// является <c>null</c> или <c>whitespace</c> или его длинна превышает <see cref="MaxLength" />.
    /// </exception>
    public UserName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Имя не может быть null или пустой строкой.", nameof(name));
        }

        name = name.Trim();

        if (name.Length > MaxLength)
        {
            throw new ArgumentException($"Имя не может быть больше {MaxLength}.", nameof(name));
        }

        Value = string.Concat(char.ToUpper(name[0])
                                  .ToString(),
                              name.AsSpan(1));
    }

    /// <summary>
    /// Возвращает значение наименования.
    /// </summary>
    /// <value>
    /// Значение наименования.
    /// </value>
    public string Value { get; }

    /// <summary>
    /// Возвращает набор компонентов, участвующий в сравнении.
    /// </summary>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
