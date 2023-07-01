using Microservice.WebApi.Infrastructure.Options;
using Microservice.WebApi.Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Microservice.WebApi.Infrastructure.IoC;

/// <summary>
/// Содержит набор методов расширения для регистрации служб аутентификации в контейнере внедрения зависимостей.
/// </summary>
internal static class AuthenticationExtensions
{
    /// <summary>
    /// Добавляет службы аутентификации в контейнер внедрения зависимостей.
    /// </summary>
    /// <param name="services">Коллекция дескрипторов службы.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="services" /> равен <c>null</c>.
    /// </exception>
    /// <returns>Коллекция дескрипторов службы.</returns>
    public static IServiceCollection AddDefaultAuthentication(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
        AddOptions(services);
        AddAuthentication(services);
        AddPermissions(services);
        return services;
    }

    private static void AddAuthentication(IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
                           options =>
                           {
                               options.SlidingExpiration = true;
                               options.ForwardChallenge = OpenIdConnectDefaults.AuthenticationScheme;
                           })
                .AddOpenIdConnect()
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme);

        services.AddAuthorization(options =>
        {
            var schemePolicyBuilder = new AuthorizationPolicyBuilder(CookieAuthenticationDefaults.AuthenticationScheme);

            var authorizationPolicy = schemePolicyBuilder.RequireAuthenticatedUser()
                                                         .Build();
            options.AddPolicy(AuthorizationPolicies.CookiesPolicy, authorizationPolicy);
        });
        services.AddSingleton<IConfigureOptions<JwtBearerOptions>, JwtBearerOptionsConfigure>();
        services.AddSingleton<IConfigureOptions<OpenIdConnectOptions>, HealthChecksOpenIdConnectOptionsConfigure>();
    }

    private static void AddOptions(IServiceCollection services)
    {
        services.AddOptions<IdentityProviderOptions.AuthorityOptions>()
                .BindConfiguration(IdentityProviderOptions.AuthorityOptions.SectionPath)
                .ValidateDataAnnotations()
                .ValidateOnStart();

        services.AddOptions<IdentityProviderOptions.AudienceOptions>()
                .BindConfiguration(IdentityProviderOptions.AudienceOptions.SectionPath)
                .ValidateDataAnnotations()
                .ValidateOnStart();

        services.AddOptions<IdentityProviderOptions.ClientOptions>()
                .BindConfiguration(IdentityProviderOptions.ClientOptions.SectionPath)
                .ValidateDataAnnotations()
                .ValidateOnStart();

        services.AddOptions<IdentityProviderOptions.ScopesOptions>()
                .BindConfiguration(IdentityProviderOptions.ScopesOptions.SectionPath)
                .ValidateDataAnnotations()
                .ValidateOnStart();
    }

    private static void AddPermissions(IServiceCollection services)
    {
        // services.AddPermissions<Permission>();
        // services.AddEnrichPrincipalService();
        // services.AddUserClient();
    }
}
