using Domain.Core;
using Domain.Core.Primitives;

namespace CourierService.Domain.Entities.Dictionaries;

/// <summary>
/// Представляет сущность метода оплаты заказа.
/// </summary>
public class PaymentMethod : Entity<SequentialGuid>
{
    /// <summary>
    /// Возвращает метод оплаты: Карта.
    /// </summary>
    public static readonly PaymentMethod Card = new(Guid.Parse("7373f370-6206-41c7-b4e7-91caddf1a35a"), "Card", 1);

    /// <summary>
    /// Возвращает метод оплаты: Наличные.
    /// </summary>
    public static readonly PaymentMethod Cash = new(Guid.Parse("d353d9a8-b9e2-4b8e-9207-e898ef328b52"), "Cash", 0);

    /// <summary>
    /// Возвращает метод оплаты: Онлайн.
    /// </summary>
    public static readonly PaymentMethod Online = new(Guid.Parse("424b93cd-ca77-4bb5-b20b-e0f1201bc350"), "Online", 2);

    public static readonly Dictionary<int, PaymentMethod> PaymentMethods = new()
    {
        [Card.Code] = Card,
        [Cash.Code] = Cash,
        [Online.Code] = Online
    };

    public PaymentMethod(SequentialGuid id, string name, int code)
        : base(id)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Code = code;
    }

    /// <summary>
    /// Возвращает код метода оплаты.
    /// </summary>
    public int Code { get; set; }

    /// <summary>
    /// Возвращает название метода оплаты.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Возвращает метод оплаты заказа, связанный с данным кодом.
    /// </summary>
    /// <param name="code">Код метода оплаты.</param>
    /// <exception cref="ArgumentException">
    /// Возникает, если <see cref="code" /> равен <c>null</c>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Возникает, если не удалось найти <see cref="PaymentMethod" /> по указанному <paramref name="code" />.
    /// </exception>
    public static PaymentMethod FromValue(int code)
    {
        if (PaymentMethods.TryGetValue(code, out var creationReason))
        {
            return creationReason;
        }

        throw new InvalidOperationException($"Не удалось найти метод оплаты с указанным кодом {code}");
    }

    /// <summary>
    /// Возвращает все значения перечисления.
    /// </summary>
    public static IReadOnlyCollection<PaymentMethod> GetAllValues() => PaymentMethods.Values;
}
