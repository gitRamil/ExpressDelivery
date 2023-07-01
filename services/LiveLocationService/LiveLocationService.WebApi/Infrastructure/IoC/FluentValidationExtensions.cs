using FluentValidation;
using LiveLocationService.Application.Behaviors;

namespace LiveLocationService.WebApi.Infrastructure.IoC;
/// <summary>
/// Содержит набор методов расширения для регистрации служб FluentValidation в контейнере внедрения зависимостей.
/// </summary>
internal static class FluentValidationExtensions
{
    /// <summary>
    /// Добавляет службы FluentValidation в контейнер внедрения зависимостей.
    /// </summary>
    /// <param name="services">Коллекция дескрипторов службы.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="services" /> равен <c>null</c>.
    /// </exception>
    /// <returns>Коллекция дескрипторов службы.</returns>
    public static IServiceCollection AddDefaultFluentValidation(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(nameof(services));

        // Todo Решить, что делать
        // https://docs.fluentvalidation.net/en/latest/aspnet.html#automatic-validation
        // services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblies(new[]
        {
            typeof(ValidationBehaviour<,>).Assembly
        });
        return services;
    }
}
