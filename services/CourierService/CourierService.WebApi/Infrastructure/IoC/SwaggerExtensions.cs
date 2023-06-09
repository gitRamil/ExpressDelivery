using CourierService.WebApi.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace CourierService.WebApi.Infrastructure.IoC;
/// <summary>
/// Содержит набор методов расширения для регистрации служб Swagger в контейнере внедрения зависимостей.
/// </summary>
internal static class SwaggerExtensions
{
    /// <summary>
    /// Добавляет службы Swagger в контейнер внедрения зависимостей.
    /// </summary>
    /// <param name="services">Коллекция дескрипторов службы.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="services" /> равен <c>null</c>.
    /// </exception>
    /// <returns>Коллекция дескрипторов службы.</returns>
    public static IServiceCollection AddDefaultSwagger(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(nameof(services));

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerGenOptionsConfigure>();
        services.AddSingleton<IConfigureOptions<SwaggerUIOptions>, SwaggerUiOptionsConfigure>();
        services.AddDateOnlyTimeOnlyStringConverters();
        return services;
    }
}
