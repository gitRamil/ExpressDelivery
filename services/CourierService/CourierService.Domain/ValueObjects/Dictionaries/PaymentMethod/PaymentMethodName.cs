using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Domain.Core;

namespace CourierService.Domain.ValueObjects.Dictionaries.PaymentMethod;

/// <summary>
/// Представляет наименование метода оплаты.
/// </summary>
[DebuggerDisplay("{_name}")]
public sealed class PaymentMethodName : ValueObject
{
    /// <summary>
    /// Представляет максимальную длину наименования метода оплаты.
    /// </summary>
    public const int MaxLength = 250;

    private readonly string _name;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="PaymentMethodName" />.
    /// </summary>
    /// <param name="name">Наименование метода оплаты.</param>
    /// <exception cref="ArgumentException">
    /// Возникает, если <paramref name="name" />
    /// является <c>null</c> или <c>whitespace</c> или его длина превышает <see cref="MaxLength" />.
    /// </exception>
    public PaymentMethodName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Наименование метода оплаты не может быть null или пустой строкой.", nameof(name));
        }

        if (name.Length > MaxLength)
        {
            throw new ArgumentException($"Наименование метода оплаты не может быть больше {MaxLength}.", nameof(name));
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
    /// Выполняет неявное преобразование из <see cref="PaymentMethodName" /> в <see cref="string" />.
    /// </summary>
    /// <param name="obj">Наименование метода оплаты.</param>
    [return: NotNullIfNotNull(nameof(obj))]
    public static implicit operator string?(PaymentMethodName? obj) => obj?._name;
}
