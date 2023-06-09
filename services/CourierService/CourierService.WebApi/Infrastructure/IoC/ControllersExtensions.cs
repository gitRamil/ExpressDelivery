using System.Text.Json.Serialization;

namespace CourierService.WebApi.Infrastructure.IoC;

/// <summary>
/// Содержит набор методов расширения для регистрации служб MVC в контейнере внедрения зависимостей.
/// </summary>
internal static class ControllersExtensions
{
    /// <summary>
    /// Добавляет службы MVC в контейнер внедрения зависимостей.
    /// </summary>
    /// <param name="services">Коллекция дескрипторов службы.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="services" /> равен <c>null</c>.
    /// </exception>
    /// <returns>Коллекция дескрипторов службы.</returns>
    public static IServiceCollection AddDefaultControllers(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(nameof(services));

        services.AddControllers()
                .AddJsonOptions(p => p.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);
        return services;
    }
}
