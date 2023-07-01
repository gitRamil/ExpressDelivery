using AuthorizationServer.Application.Abstractions;
using AuthorizationServer.Application.Exceptions;
using AuthorizationServer.Domain.Entities;
using Domain.Core.Primitives;

namespace AuthorizationServer.Application.UseCases.DeleteUser;
/// <summary>
/// Представляет обработчик, осуществляющий удаление пользователя.
/// </summary>
public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly IAppDbContext _context;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="DeleteUserCommandHandler" />.
    /// </summary>
    /// <param name="context">Контекст БД.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="context" /> равен <c>null</c>.
    /// </exception>
    public DeleteUserCommandHandler(IAppDbContext context) => _context = context ?? throw new ArgumentNullException(nameof(context));

    /// <inheritdoc />
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="request" /> равен <c>null</c>.
    /// </exception>
    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
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

        _context.Users.Remove(user);

        await _context.SaveChangesAsync(cancellationToken)
                      .ConfigureAwait(false);
    }
}
