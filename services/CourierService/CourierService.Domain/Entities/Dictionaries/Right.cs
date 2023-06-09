using Domain.Core;
using Domain.Core.Primitives;

namespace CourierService.Domain.Entities.Dictionaries;

/// <summary>
/// Представляет сущность прав пользователя.
/// </summary>
public class Right : Entity<SequentialGuid>
{
    /// <summary>
    /// Возвращает право пользователя: Администратор.
    /// </summary>
    public static readonly Right Admin = new(Guid.Parse("3dfcd6f3-1775-4e1b-91db-fdccea3f83eb"), "Admin", 1);

    /// <summary>
    /// Возвращает право пользователя: Курьер.
    /// </summary>
    public static readonly Right Courier = new(Guid.Parse("60eb98f3-9f8c-4c12-93d4-66f208caa6f6"), "Courier", 2);

    /// <summary>
    /// Возвращает право пользователя: Пользователь.
    /// </summary>
    public static readonly Right User = new(Guid.Parse("e10222c4-7723-498b-8bf4-83252378e0c9"), "User", 0);

    private static readonly Dictionary<int, Right> Rights = new()
    {
        [User.Code] = User,
        [Admin.Code] = Admin,
        [Courier.Code] = Courier
    };

    public Right(SequentialGuid id, string name, int code)
        : base(id)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Code = code;
    }

    /// <summary>
    /// Возвращает код доступа.
    /// </summary>
    public int Code { get; set; }

    /// <summary>
    /// Возвращает название доступа.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Возвращает право пользователя, связанное с данным кодом.
    /// </summary>
    /// <param name="code">Право пользователя.</param>
    /// <exception cref="ArgumentException">
    /// Возникает, если <see cref="code" /> равен <c>null</c>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Возникает, если не удалось найти <see cref="Right" /> по указанному <paramref name="code" />.
    /// </exception>
    public static Right FromValue(int code)
    {
        if (Rights.TryGetValue(code, out var creationReason))
        {
            return creationReason;
        }

        throw new InvalidOperationException($"Не удалось найти право пользователя с указанным кодом {code}");
    }

    /// <summary>
    /// Возвращает все значения перечисления.
    /// </summary>
    public static IReadOnlyCollection<Right> GetAllValues() => Rights.Values;
}
