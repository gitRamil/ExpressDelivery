using Flurl;
using CourierService.Application.Behaviors;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Extensions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using CourierService.WebApi.Infrastructure.Security;

namespace CourierService.WebApi.Infrastructure.Options;
/// <summary>
/// Представляет конфигурацию для <see cref="SwaggerGenOptions" />.
/// </summary>
internal sealed class SwaggerGenOptionsConfigure : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IOptions<IdentityProviderOptions.AuthorityOptions> _authorityOptions;
    private readonly IOptions<IdentityProviderOptions.ScopesOptions> _scopesOptions;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="SwaggerGenOptionsConfigure" />.
    /// </summary>
    /// <param name="authorityOptions">Опции Authority.</param>
    /// <param name="scopesOptions">Опции Scopes.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="authorityOptions" /> или <paramref name="scopesOptions" /> равен <c>null</c>.
    /// </exception>
    public SwaggerGenOptionsConfigure(IOptions<IdentityProviderOptions.AuthorityOptions> authorityOptions, IOptions<IdentityProviderOptions.ScopesOptions> scopesOptions)
    {
        _authorityOptions = authorityOptions ?? throw new ArgumentNullException(nameof(authorityOptions));
        _scopesOptions = scopesOptions ?? throw new ArgumentNullException(nameof(scopesOptions));
    }

    /// <inheritdoc />
    public void Configure(SwaggerGenOptions swaggerGenOptions)
    {
        swaggerGenOptions.SchemaGeneratorOptions.SupportNonNullableReferenceTypes = true;
        swaggerGenOptions.UseDateOnlyTimeOnlyStringConverters();

        swaggerGenOptions.SwaggerDoc("v1",
                                     new OpenApiInfo
                                     {
                                         Title = "CourierService API здесь указать заголовок",
                                         Description = "CourierService здесь указать описание"
                                     });

        AddSecurity(swaggerGenOptions, _authorityOptions, _scopesOptions);
        AddXmlComments(swaggerGenOptions);
    }

    private static void AddSecurity(SwaggerGenOptions target, IOptions<IdentityProviderOptions.AuthorityOptions> authorityOptions, IOptions<IdentityProviderOptions.ScopesOptions> scopesOptions)
    {
        var name = SecuritySchemeType.OAuth2.GetDisplayName()!;

        var authorizationUrl = authorityOptions.Value.Authority.AppendPathSegment(IdentityProviderUrlConstants.Authorization);
        var tokenUrl = authorityOptions.Value.Authority.AppendPathSegment(IdentityProviderUrlConstants.Token);

        target.AddSecurityDefinition(name,
                                     new OpenApiSecurityScheme
                                     {
                                         Type = SecuritySchemeType.OAuth2,
                                         Flows = new OpenApiOAuthFlows
                                         {
                                             AuthorizationCode = new OpenApiOAuthFlow
                                             {
                                                 AuthorizationUrl = new Uri(authorizationUrl),
                                                 TokenUrl = new Uri(tokenUrl),
                                                 Scopes = scopesOptions.Value.Scopes
                                             }
                                         }
                                     });

        target.OperationFilter<AuthorizeCheckOperationFilter>();
    }

    private static void AddXmlComments(SwaggerGenOptions options)
    {
        var webApiXmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var webApiXmlPath = Path.Combine(AppContext.BaseDirectory, webApiXmlFile);

        var applicationXmlFile = $"{typeof(ValidationBehaviour<,>).Assembly.GetName().Name}.xml";
        var applicationXmlPath = Path.Combine(AppContext.BaseDirectory, applicationXmlFile);

        options.IncludeXmlComments(webApiXmlPath, true);
        options.IncludeXmlComments(applicationXmlPath);
    }
}
