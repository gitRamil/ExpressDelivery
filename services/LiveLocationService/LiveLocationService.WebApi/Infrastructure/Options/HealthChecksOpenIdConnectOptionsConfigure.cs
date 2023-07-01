using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Options;

namespace LiveLocationService.WebApi.Infrastructure.Options;
/// <summary>
/// Представляет конфигурацию для <see cref="OpenIdConnectOptions" />.
/// </summary>
internal sealed class HealthChecksOpenIdConnectOptionsConfigure : IConfigureNamedOptions<OpenIdConnectOptions>
{
    private readonly IOptions<IdentityProviderOptions.AuthorityOptions> _authorityOptions;
    private readonly IOptions<IdentityProviderOptions.ClientOptions> _clientOptions;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="HealthChecksOpenIdConnectOptionsConfigure" />.
    /// </summary>
    /// <param name="authorityOptions">Параметры конфигурации для Authority.</param>
    /// <param name="clientOptions">Параметры конфигурации для клиента.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="authorityOptions" /> или <paramref name="clientOptions" /> равен <c>null</c>.
    /// </exception>
    public HealthChecksOpenIdConnectOptionsConfigure(IOptions<IdentityProviderOptions.AuthorityOptions> authorityOptions,
                                                     IOptions<IdentityProviderOptions.ClientOptions> clientOptions)
    {
        _authorityOptions = authorityOptions ?? throw new ArgumentNullException(nameof(authorityOptions));
        _clientOptions = clientOptions ?? throw new ArgumentNullException(nameof(clientOptions));
    }

    /// <inheritdoc />
    public void Configure(string? name, OpenIdConnectOptions options)
    {
        if (name == OpenIdConnectDefaults.AuthenticationScheme)
        {
            Configure(options);
        }
    }

    /// <inheritdoc />
    public void Configure(OpenIdConnectOptions options)
    {
        const string profileScope = "profile";
        const string responseType = "code";
        options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.Authority = _authorityOptions.Value.Authority;
        options.ClientId = _clientOptions.Value.ClientId;
        options.ClientSecret = _clientOptions.Value.ClientSecret;
        options.Scope.Remove(profileScope);
        options.ResponseType = responseType;
        options.SaveTokens = true;
    }
}
