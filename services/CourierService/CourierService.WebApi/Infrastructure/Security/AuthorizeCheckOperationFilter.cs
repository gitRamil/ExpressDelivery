using System.Net;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Extensions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using static CourierService.WebApi.Infrastructure.Options.IdentityProviderOptions;

namespace CourierService.WebApi.Infrastructure.Security;

/// <summary>
/// Представляет операционный фильтр для операции.
/// </summary>
/// <seealso cref="IOperationFilter" />
internal sealed class AuthorizeCheckOperationFilter : IOperationFilter
{
    #region Data
    #region Fields
    private readonly IOptions<ScopesOptions> _scopesOptions;
    #endregion
    #endregion

    #region .ctor
    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="AuthorizeCheckOperationFilter" />.
    /// </summary>
    /// <param name="scopesOptions">Опции.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="scopesOptions" /> равен <c>null</c>.
    /// </exception>
    public AuthorizeCheckOperationFilter(IOptions<ScopesOptions> scopesOptions) => _scopesOptions = scopesOptions ?? throw new ArgumentNullException(nameof(scopesOptions));
    #endregion

    #region IOperationFilter members
    /// <inheritdoc />
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var methodInfo = context.MethodInfo;

        var hasAuthorize = methodInfo.DeclaringType?.GetCustomAttribute<AuthorizeAttribute>(true) != null || methodInfo.GetCustomAttribute<AuthorizeAttribute>(true) != null;

        if (hasAuthorize)
        {
            operation.Responses.TryAdd(((int)HttpStatusCode.Unauthorized).ToString(),
                                       new OpenApiResponse
                                       {
                                           Description = HttpStatusCode.Unauthorized.ToString()
                                       });

            operation.Responses.TryAdd(((int)HttpStatusCode.Forbidden).ToString(),
                                       new OpenApiResponse
                                       {
                                           Description = HttpStatusCode.Forbidden.ToString()
                                       });

            operation.Security = new List<OpenApiSecurityRequirement>
            {
                new()
                {
                    [new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = SecuritySchemeType.OAuth2.GetDisplayName()!
                        }
                    }] = _scopesOptions.Value.Scopes.Select(s => s.Key)
                                       .ToArray()
                }
            };
        }
    }
    #endregion
}
