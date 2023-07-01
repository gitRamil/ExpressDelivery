using FluentValidation;
using ValidationException = LiveLocationService.Application.Exceptions.ValidationException;

namespace LiveLocationService.Application.Behaviors;
/// <summary>
/// Реализует сквозную функциональность по проверке достоверности <see cref="IRequest&lt;TResponse&gt;" />.
/// </summary>
/// <typeparam name="TRequest">Тип запроса.</typeparam>
/// <typeparam name="TResponse">Тип ответа.</typeparam>
public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IBaseRequest
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="ValidationBehaviour{TRequest, TResponse}" />.
    /// </summary>
    /// <param name="validators">Набор средств проверки.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="validators" /> равен <c>null</c>.
    /// </exception>
    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators) => _validators = validators ?? throw new ArgumentNullException(nameof(validators));

    /// <inheritdoc />
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)))
                                              .ConfigureAwait(false);

            var failures = validationResults.Where(r => r.Errors.Any())
                                            .SelectMany(r => r.Errors)
                                            .ToList();

            if (failures.Any())
            {
                var errors = failures.GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                                     .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
                throw new Exceptions.ValidationException(errors);
            }
        }

        return await next()
                   .ConfigureAwait(false);
    }
}
