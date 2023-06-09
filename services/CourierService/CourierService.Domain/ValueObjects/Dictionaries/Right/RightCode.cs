using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Domain.Core;

namespace CourierService.Domain.ValueObjects.Dictionaries.Right;

/// <summary>
/// Представляет код прав доступа.
/// </summary>
[DebuggerDisplay("{_code}")]
public sealed class RightCode : ValueObject
{
    /// <summary>
    /// Представляет максимальную длину кода прав доступа.
    /// </summary>
    public const int MaxLength = 100;

    private readonly string _code;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="RightCode" />.
    /// </summary>
    /// <param name="code">Код прав доступа.</param>
    /// <exception cref="ArgumentException">
    /// Возникает, если <paramref name="code" />
    /// равен <c>null</c> или пустой строке или его длина превышает <see cref="MaxLength" />.
    /// </exception>
    public RightCode(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            throw new ArgumentException("Код прав доступа не может быть null или пустой строкой.", nameof(code));
        }

        code = code.Trim();

        if (code.Length > MaxLength)
        {
            throw new ArgumentException($"Код прав доступа не может быть больше {MaxLength}.", nameof(code));
        }

        _code = code;
    }

    /// <inheritdoc />
    public override string ToString() => _code;

    /// <summary>
    /// Возвращает набор компонентов, участвующий в сравнении.
    /// </summary>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return _code;
    }

    /// <summary>
    /// Выполняет неявное преобразование из <see cref="RightCode" /> в <see cref="string" />.
    /// </summary>
    /// <param name="obj">Код прав доступа.</param>
    [return: NotNullIfNotNull("obj")]
    public static implicit operator string?(RightCode? obj) => obj?._code;
}
