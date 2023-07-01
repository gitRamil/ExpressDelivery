namespace LiveLocationService.WebApi.Infrastructure.Security;

/// <summary>
/// Содержит набор констант URL для конечных точек IdentityProvider.
/// </summary>
internal static class IdentityProviderUrlConstants
{
    /// <summary>
    /// Возвращает адрес конечной точки авторизации.
    /// </summary>
    public const string Authorization = "protocol/openid-connect/auth";

    /// <summary>
    /// Возвращает адрес конечной точки маркера.
    /// </summary>
    public const string Token = "protocol/openid-connect/token";
}
