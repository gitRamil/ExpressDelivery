using AuthorizationServer.Application.Abstractions;
using AuthorizationServer.Application.Dtos;
using AuthorizationServer.Domain.Entities;
using AuthorizationServer.Domain.ValueObjects;
using AutoMapper;
using Domain.Core.Primitives;

namespace AuthorizationServer.Application.UseCases.CreateUser;
/// <summary>
/// Представляет обработчик, осуществляющий создание пользователя.
/// </summary>
public class CreateUserHandler : IRequestHandler<CreateUserCommand, UserDto>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="CreateUserHandler" />.
    /// </summary>
    /// <param name="context">Контекст БД.</param>
    /// <param name="mapper">Объектный преобразователь.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="context" /> равен <c>null</c>.
    /// </exception>
    public CreateUserHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    /// <inheritdoc />
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="request" /> равен <c>null</c>.
    /// </exception>
    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var user = new User(SequentialGuid.Create(), new UserName(request.UserName));
        _context.Users.Add(user);

        await _context.SaveChangesAsync(cancellationToken)
                      .ConfigureAwait(false);
        return _mapper.Map<UserDto>(user);
    }
}
