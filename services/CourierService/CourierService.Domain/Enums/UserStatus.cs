namespace CourierService.Domain.Enums;

/// <summary>
/// Содержит набор значений, представляющих статусы пользователя <see cref="User" />.
/// </summary>
public enum UserStatus
{
    /// <summary>
    /// Неподтвержденный.
    /// </summary>
    Unverified = 0,

    /// <summary>
    /// Подтвержденный.
    /// </summary>
    Verified = 1
}
