using CourierService.Application.Abstractions;
using CourierService.Application.Exceptions;
using CourierService.Domain.Entities;
using CourierService.Domain.ValueObjects;
using Domain.Core.Primitives;

namespace CourierService.Application.UseCases.ConfirmUser;
/// <summary>
/// Представляет обработчик, осуществляющий подтверждение пользователя.
/// </summary>
public class ConfirmUserHandler : IRequestHandler<ConfirmUserCommand>
{
    private readonly IAppDbContext _context;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="ConfirmUserHandler" />.
    /// </summary>
    /// <param name="context">Контекст БД.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="context" /> равен <c>null</c>.
    /// </exception>
    public ConfirmUserHandler(IAppDbContext context) => _context = context ?? throw new ArgumentNullException(nameof(context));

    /// <inheritdoc />
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="request" /> равен <c>null</c>.
    /// </exception>
    public async Task Handle(ConfirmUserCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var user = await _context.Users.FindAsync(new object?[]
                                                  {
                                                      (SequentialGuid)request.UserId
                                                  },
                                                  cancellationToken)
                                 .ConfigureAwait(false);

        if (user is null)
        {
            throw new NotFoundException(typeof(User), request.UserId);
        }

        user.Confirm((UserPhone)request.UserPhone!);

        await _context.SaveChangesAsync(cancellationToken)
                      .ConfigureAwait(false);
    }
}
