namespace CourierService.Application.Dtos.Common;

/// <summary>
/// Представляет результат постраничного запроса.
/// </summary>
/// <typeparam name="T">Элемент коллекции.</typeparam>
public record DataResult<T>
{
    private const int MinTotalCount = 0;

    /// <summary>
    /// Инициализирует новый экземпляр типа <see cref="DataResult{T}" />.
    /// </summary>
    /// <param name="items">Контекст БД.</param>
    /// <param name="total">Маппер.</param>
    /// <exception cref="ArgumentException">
    /// Возникает, если <paramref name="total" /> меньше <see cref="MinTotalCount" />.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="items" /> равен <c>null</c>.
    /// </exception>
    public DataResult(IReadOnlyList<T> items, int total)
    {
        if (total < MinTotalCount)
        {
            throw new ArgumentException($"Общее число элементов не может быть меньше {MinTotalCount}.", nameof(total));
        }

        Items = items ?? throw new ArgumentNullException(nameof(items));
        Total = total;
    }

    /// <summary>
    /// Возвращает список элементов.
    /// </summary>
    public IReadOnlyList<T> Items { get; }
    
    /// <summary>
    /// Возвращает общее число элементов.
    /// </summary>
    public int Total { get; }
}
