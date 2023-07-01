using AuthorizationServer.Domain.Entities;
using AuthorizationServer.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthorizationServer.Infrastructure.Persistence.Configurations;
/// <summary>
/// Представляет настройку конфигурации для типа <see cref="User" />.
/// </summary>
internal sealed class UserConfiguration : EntityTypeConfigurationBase<User>
{
    /// <summary>
    /// Вызывается при выполнении конфигурации сущности типа <see cref="User" />.
    /// </summary>
    /// <param name="builder">Строитель, используемый при конфигурации сущности.</param>
    protected override void OnConfigure(EntityTypeBuilder<User> builder)
    {
        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(100)
               .HasConversion(o => o.Value, s => new UserName(s));

        builder.Property(p => p.Phone)
               .HasMaxLength(15)
               .HasConversion(o => (string?)o, s => (UserPhone?)s);

        builder.HasIndex(p => p.Phone)
               .IsUnique();
        builder.ToTable("users");
    }
}
