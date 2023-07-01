using AutoMapper;
using Domain.Core.Primitives;
using LiveLocationService.Application.Abstractions;
using LiveLocationService.Application.Dtos;
using LiveLocationService.Application.Exceptions;
using LiveLocationService.Domain.Entities;

namespace LiveLocationService.Application.UseCases.GetUserById;
/// <summary>
/// Представляет обработчик, осуществляющий возвращение пользователя.
/// </summary>
public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="GetUserByIdQueryHandler" />.
    /// </summary>
    /// <param name="context">Контекст БД.</param>
    /// <param name="mapper">Объектный преобразователь.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="context" /> равен <c>null</c>.
    /// </exception>
    public GetUserByIdQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    /// <inheritdoc />
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="request" /> равен <c>null</c>.
    /// </exception>
    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
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

        return _mapper.Map<UserDto>(user);
    }
}
