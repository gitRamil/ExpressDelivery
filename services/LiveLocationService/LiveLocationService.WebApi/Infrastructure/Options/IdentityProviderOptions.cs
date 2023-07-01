using System.ComponentModel.DataAnnotations;

namespace LiveLocationService.WebApi.Infrastructure.Options;
/// <summary>
/// Представляет конфигурацию для IdentityProvider.
/// </summary>
internal static class IdentityProviderOptions
{
    /// <summary>
    /// Возвращает имя секции в файле конфигурации.
    /// </summary>
    private const string RootSectionPath = "IdentityProvider";

    /// <summary>
    /// Представляет конфигурацию для Audience.
    /// </summary>
    internal sealed class AudienceOptions
    {
        /// <summary>
        /// Возвращает имя секции в файле конфигурации.
        /// </summary>
        internal const string SectionPath = RootSectionPath;

        /// <summary>
        /// Возвращает или устанавливает аудиторию.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string Audience { get; set; } = default!;
    }

    /// <summary>
    /// Представляет конфигурацию для Authority.
    /// </summary>
    internal sealed class AuthorityOptions
    {
        /// <summary>
        /// Возвращает имя секции в файле конфигурации.
        /// </summary>
        internal const string SectionPath = RootSectionPath;

        /// <summary>
        /// Возвращает или устанавливает сервер авторизации.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        [Url]
        public string Authority { get; set; } = default!;
    }

    /// <summary>
    /// Представляет конфигурацию клиента.
    /// </summary>
    internal sealed class ClientOptions
    {
        /// <summary>
        /// Возвращает имя секции в файле конфигурации.
        /// </summary>
        public const string SectionPath = RootSectionPath + ":Client";

        /// <summary>
        /// Возвращает или устанавливает идентификатор клиента.
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string ClientId { get; set; } = default!;

        /// <summary>
        /// Возвращает или устанавливает секрет клиента
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string ClientSecret { get; set; } = default!;
    }

    /// <summary>
    /// Представляет конфигурацию для Scopes.
    /// </summary>
    internal sealed class ScopesOptions
    {
        /// <summary>
        /// Возвращает имя секции в файле конфигурации.
        /// </summary>
        internal const string SectionPath = RootSectionPath;

        /// <summary>
        /// Возвращает или устанавливает набор областей доступа.
        /// </summary>
        [Required]
        [MinLength(1)]
        public Dictionary<string, string> Scopes { get; set; } = default!;
    }
}
