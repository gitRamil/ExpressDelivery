using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Domain.Core;

namespace CourierService.Domain.ValueObjects.Dictionaries.Right;

/// <summary>
/// Представляет наименование прав доступа.
/// </summary>
[DebuggerDisplay("{_name}")]
public sealed class RightName : ValueObject
{
    /// <summary>
    /// Представляет максимальную длину наименования прав доступа.
    /// </summary>
    public const int MaxLength = 250;

    private readonly string _name;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="RightName" />.
    /// </summary>
    /// <param name="name">Наименование прав доступа.</param>
    /// <exception cref="ArgumentException">
    /// Возникает, если <paramref name="name" />
    /// является <c>null</c> или <c>whitespace</c> или его длина превышает <see cref="MaxLength" />.
    /// </exception>
    public RightName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Наименование прав доступа не может быть null или пустой строкой.", nameof(name));
        }

        if (name.Length > MaxLength)
        {
            throw new ArgumentException($"Наименование прав доступа не может быть больше {MaxLength}.", nameof(name));
        }

        _name = name;
    }

    /// <inheritdoc />
    public override string ToString() => _name;

    /// <summary>
    /// Возвращает набор компонентов, участвующий в сравнении.
    /// </summary>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return _name;
    }

    /// <summary>
    /// Выполняет неявное преобразование из <see cref="RightName" /> в <see cref="string" />.
    /// </summary>
    /// <param name="obj">Наименование прав доступа.</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static implicit operator string?(RightName? obj) => obj?._name;
}
