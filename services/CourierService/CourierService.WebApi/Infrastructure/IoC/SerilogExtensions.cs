using Serilog;

namespace CourierService.WebApi.Infrastructure.IoC;
/// <summary>
/// Содержит набор методов расширения для регистрации служб Serilog в контейнере внедрения зависимостей.
/// </summary>
internal static class SerilogExtensions
{
    /// <summary>
    /// Добавляет службы Serilog в контейнер внедрения зависимостей.
    /// </summary>
    /// <param name="builder">Строитель веб приложения и служб.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="builder" /> равен <c>null</c>.
    /// </exception>
    /// <returns>Коллекция дескрипторов службы.</returns>
    public static IServiceCollection AddDefaultSerilog(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(nameof(builder));

        Environment.SetEnvironmentVariable(nameof(AppDomain.CurrentDomain.BaseDirectory),
                                           AppDomain.CurrentDomain.BaseDirectory);
        Log.Logger = new LoggerConfiguration()
                     .ReadFrom.Configuration(builder.Configuration)
                     .Enrich.FromLogContext()
                     .CreateLogger();

        builder.Host.UseSerilog();

        return builder.Services;
    }
}
