using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace LiveLocationService.WebApi.Infrastructure.Middleware;
/// <summary>
/// Содержит настройки конфигурации для конечной точки HealthChecks.
/// </summary>
public static class HealthCheckExtensions
{
    /// <summary>
    /// Регистрирует конечную точку проверки здоровья.
    /// </summary>
    /// <param name="target">Строитель конечной точки маршрутизации приложения.</param>
    /// <param name="endpointUrl">URL для доступа к информации о состоянии здоровья.</param>
    /// <param name="tagFilter">Фильтр.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="target" /> равен <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Возникает, если <paramref name="endpointUrl" /> равен <c>null</c>.
    /// </exception>
    public static IEndpointConventionBuilder MapCustomHealthChecks(this IEndpointRouteBuilder target, string endpointUrl, Func<string, bool>? tagFilter = null)
    {
        if (target == null)
        {
            throw new ArgumentNullException(nameof(target));
        }

        if (string.IsNullOrWhiteSpace(endpointUrl))
        {
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(endpointUrl));
        }

        var endpointConventionBuilder = target.MapHealthChecks(endpointUrl,
                                                               new HealthCheckOptions
                                                               {
                                                                   ResultStatusCodes = GetResultStatusCodes(),
                                                                   Predicate = GetFilter(tagFilter),
                                                                   ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                                                               });
        return endpointConventionBuilder;
    }

    private static Func<HealthCheckRegistration, bool> GetFilter(Func<string, bool>? tagFilter)
    {
        return tagFilter is null ? _ => true : check => check.Tags.Any(tagFilter);
    }

    private static IDictionary<HealthStatus, int> GetResultStatusCodes()
    {
        return new Dictionary<HealthStatus, int>
        {
            [HealthStatus.Healthy] = StatusCodes.Status200OK,
            [HealthStatus.Degraded] = StatusCodes.Status500InternalServerError,
            [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
        };
    }
}
