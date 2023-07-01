using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace AuthorizationServer.WebApi.Infrastructure.Options;
/// <summary>
/// Представляет конфигурацию для <see cref="JwtBearerOptions" />.
/// </summary>
internal sealed class JwtBearerOptionsConfigure : IConfigureNamedOptions<JwtBearerOptions>
{
    private readonly IOptions<IdentityProviderOptions.AudienceOptions> _audienceOptions;
    private readonly IOptions<IdentityProviderOptions.AuthorityOptions> _authorityOptions;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="JwtBearerOptionsConfigure" />.
    /// </summary>
    /// <param name="authorityOptions">Опции.</param>
    /// <param name="audienceOptions">Опции.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="authorityOptions" /> или <paramref name="audienceOptions" /> равен <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Возникает, если <paramref name="authorityOptions.Value"></paramref> или
    /// <paramref name="audienceOptions.Value"></paramref> содержит значение равное
    /// <c>null</c>.
    /// </exception>
    public JwtBearerOptionsConfigure(IOptions<IdentityProviderOptions.AuthorityOptions> authorityOptions, IOptions<IdentityProviderOptions.AudienceOptions> audienceOptions)
    {
        if (authorityOptions == null)
        {
            throw new ArgumentNullException(nameof(authorityOptions));
        }

        if (authorityOptions.Value == null)
        {
            throw new ArgumentException($"{nameof(authorityOptions)}.{nameof(authorityOptions.Value)} не может быть null.", nameof(authorityOptions));
        }

        if (audienceOptions == null)
        {
            throw new ArgumentNullException(nameof(audienceOptions));
        }

        if (audienceOptions.Value == null)
        {
            throw new ArgumentException($"{nameof(audienceOptions)}.{nameof(audienceOptions.Value)} не может быть null.", nameof(audienceOptions));
        }

        _authorityOptions = authorityOptions;
        _audienceOptions = audienceOptions;
    }

    /// <inheritdoc />
    public void Configure(string? name, JwtBearerOptions options)
    {
        if (name == JwtBearerDefaults.AuthenticationScheme)
        {
            Configure(options);
        }
    }

    /// <inheritdoc />
    public void Configure(JwtBearerOptions options)
    {
        options.Authority = _authorityOptions.Value.Authority;
        options.Audience = _audienceOptions.Value.Audience;

        options.Events = new JwtBearerEvents
        {
            // OnTokenValidated = async context =>
            // {
            //     var sp = context.HttpContext.RequestServices;
            //     var enrichPrincipalService = sp.GetRequiredService<IEnrichPrincipalService>();
            //     await enrichPrincipalService.EnrichPrincipalAsync(context.Principal!, context.HttpContext.RequestAborted);
            // }
        };
    }
}
