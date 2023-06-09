using CourierService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourierService.Infrastructure.Persistence.Configurations;

/// <summary>
/// Представляет настройку конфигурации для типа <see cref="User" />.
/// </summary>
internal class UserConfiguration : EntityTypeConfigurationBase<User>
{
    /// <summary>
    /// Вызывается при выполнении конфигурации сущности типа <see cref="User" />.
    /// </summary>
    /// <param name="builder">Строитель, используемый при конфигурации сущности.</param>
    protected override void OnConfigure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users", t => t.HasComment("Пользователи"));

        builder.Property(p => p.FirstName)
               .HasComment("Имя");

        builder.Property(p => p.LastName)
               .HasComment("Фамилия");

        builder.Property(p => p.Login)
               .HasComment("Логин пользователя");

        builder.Property(p => p.Mail)
               .HasComment("Эл. почта");

        builder.Property(p => p.PasswordHash)
               .HasComment("Хеш пароля");

        builder.Property(p => p.PasswordSalt)
               .HasComment("Соль пароля");

        builder.Property(p => p.Phone)
               .HasComment("Номер телефона");

        builder.Property(p => p.RefreshToken)
               .HasComment("Рефреш-токен");

        builder.HasOne(p => p.Right)
               .WithMany()
               .HasForeignKey("right_id");

        builder.Property("right_id")
               .HasComment("Идентификатор прав пользователя");

        builder.Property(p => p.TokenCreated)
               .HasComment("Дата создания токена");

        builder.Property(p => p.TokenExpires)
               .HasComment("Дата истечения токена");
    }
}
