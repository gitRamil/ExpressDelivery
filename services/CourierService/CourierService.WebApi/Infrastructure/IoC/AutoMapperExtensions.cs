using CourierService.Application.Profiles;

namespace CourierService.WebApi.Infrastructure.IoC;
/// <summary>
/// Содержит набор методов расширения для регистрации служб AutoMapper в контейнере внедрения зависимостей.
/// </summary>
internal static class AutoMapperExtensions
{
    /// <summary>
    /// Добавляет службы AutoMapper в контейнер внедрения зависимостей.
    /// </summary>
    /// <param name="services">Коллекция дескрипторов службы.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="services" /> равен <c>null</c>.
    /// </exception>
    /// <returns>Коллекция дескрипторов службы.</returns>
    public static IServiceCollection AddDefaultAutoMapper(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(nameof(services));
        services.AddAutoMapper(typeof(UserProfile));
        return services;
    }
}
