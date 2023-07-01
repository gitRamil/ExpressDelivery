using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace LiveLocationService.WebApi.Infrastructure.Options;
/// <summary>
/// Представляет конфигурацию для <see cref="SwaggerUIOptions" />.
/// </summary>
internal sealed class SwaggerUiOptionsConfigure : IConfigureOptions<SwaggerUIOptions>
{
    #region Data
    #region Fields
    private readonly IOptions<IdentityProviderOptions.ClientOptions> _clientOptions;
    #endregion
    #endregion

    #region .ctor
    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="SwaggerUiOptionsConfigure" />.
    /// </summary>
    /// <param name="clientOptions">Опции.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="clientOptions" /> равен <c>null</c>.
    /// </exception>
    public SwaggerUiOptionsConfigure(IOptions<IdentityProviderOptions.ClientOptions> clientOptions)
    {
        _clientOptions = clientOptions ?? throw new ArgumentNullException(nameof(clientOptions));
    }
    #endregion

    #region IConfigureOptions<SwaggerUIOptions> members
    /// <inheritdoc />
    public void Configure(SwaggerUIOptions options)
    {
        options.OAuthClientId(_clientOptions.Value.ClientId);
        options.OAuthClientSecret(_clientOptions.Value.ClientSecret);
        options.OAuthUsePkce();
    }
    #endregion
}
