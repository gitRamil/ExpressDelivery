namespace LiveLocationService.WebApi.Infrastructure.IoC;

/// <summary>
/// Содержит набор методов расширения для регистрации служб HealthChecks в контейнере внедрения зависимостей.
/// </summary>
internal static class HealthChecksExtensions
{
    /// <summary>
    /// Добавляет службы HealthChecks в контейнер внедрения зависимостей.
    /// </summary>
    /// <param name="services">Коллекция дескрипторов службы.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="services" /> равен <c>null</c>.
    /// </exception>
    /// <returns>Коллекция дескрипторов службы.</returns>
    public static IServiceCollection AddDefaultHealthChecks(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(nameof(services));

        services.AddHealthChecks()
                .AddNpgSql(provider =>
                {
                    var configuration = provider.GetRequiredService<IConfiguration>();

                    return configuration.GetConnectionString(DbConstants.ConnectionStringSectionName) ??
                           throw new ArgumentException("Строка подключения к БД не указана в конфигурации.");
                });

        services.AddHealthChecksUI()
                .AddInMemoryStorage();
        return services;
    }
}
