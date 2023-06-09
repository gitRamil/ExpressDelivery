namespace CourierService.WebApi.Infrastructure.Security;

/// <summary>
/// Содержит набор констант для политик авторизации.
/// </summary>
internal static class AuthorizationPolicies
{
    /// <summary>
    /// Возвращает имя политики основанной на cookies.
    /// </summary>
    public const string CookiesPolicy = "CookiesPolicy";
}
