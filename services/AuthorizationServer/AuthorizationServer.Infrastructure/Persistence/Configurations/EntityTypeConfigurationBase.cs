using Domain.Core;
using Domain.Core.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthorizationServer.Infrastructure.Persistence.Configurations;
/// <summary>
/// Представляет базовую реализацию для настройки конфигурации объектно реляционного отображения для типа <see cref="T" />.
/// </summary>
/// <typeparam name="T">Тип сущности.</typeparam>
/// <seealso cref="IEntityTypeConfiguration{T}" />
internal abstract class EntityTypeConfigurationBase<T> : IEntityTypeConfiguration<T> where T : Entity<SequentialGuid>
{
    /// <summary>
    /// Вызывается при выполнении конфигурации сущности типа <see cref="T" />.
    /// </summary>
    /// <param name="builder">Строитель, используемый при конфигурации сущности.</param>
    protected abstract void OnConfigure(EntityTypeBuilder<T> builder);

    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.Property(p => p.Id)
               .HasConversion(p => (Guid)p, p => p);
        OnConfigure(builder);
    }
}
