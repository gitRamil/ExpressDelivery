using CourierService.Application.Abstractions;

namespace CourierService.WebApi.Infrastructure.Security;
/// <summary>
/// Представляет текущего пользователя.
/// </summary>
/// <seealso cref="ICurrentUser" />
public sealed class CurrentUser : ICurrentUser
{
    #region Data
    #region Fields
    private readonly IHttpContextAccessor _httpContextAccessor;
    #endregion
    #endregion

    #region .ctor
    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="CurrentUser" />.
    /// </summary>
    /// <param name="httpContextAccessor">HTTP контекст.</param>
    /// <exception cref="System.ArgumentNullException">
    /// Возникает, если <paramref name="httpContextAccessor" />
    /// равен <c>false</c>.
    /// </exception>
    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }
    #endregion

    #region ICurrentUser members
    /// <summary>
    /// Возвращает идентификатор текущего пользователя.
    /// </summary>
    /// <returns>Идентификатор текущего пользователя или <see cref="Guid.Empty" />, если пользователь является неаутентифицированным.</returns>
    public Guid Id
    {
        get
        {
            const string idClaimType = "id";

            var idClaim = _httpContextAccessor.HttpContext?.User.FindFirst(p => p.Type == idClaimType);

            if (idClaim != null)
            {
                return Guid.Parse(idClaim.Value);
            }

            return Guid.Empty;
        }
    }

    /// <summary>
    /// Возвращает признак, определяющий является ли текущий пользователь аутентифицированным.
    /// </summary>
    /// <returns>
    /// <c>true</c> если пользователь является аутентифицированным; в противном случае, <c>false</c>.
    /// </returns>
    public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated == true;
    #endregion
}
