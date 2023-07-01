using System.Linq.Expressions;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace CourierService.Application.Extensions;

/// <summary>
/// Содержит набор методов расширения для работы с фильтрами запросов <see cref="IQueryable{T}" />.
/// </summary>
public static class QueryFilterExtensions
{
    /// <summary>
    /// Применяет фильтр "содержится в коллекции", если коллекция не пуста.
    /// </summary>
    /// <param name="query">Запрос.</param>
    /// <param name="collection">Коллекция, содержащая элементы для фильтрации.</param>
    /// <param name="propertySelector">Выражение выбора элемента, к которому применять фильтр.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="query" /> или <paramref name="propertySelector" /> равен <c>null</c>.
    /// </exception>
    public static IQueryable<T> ApplyContainsFilter<T, TCollection, TCollectionItem>(this IQueryable<T> query,
                                                                                     TCollection? collection,
                                                                                     Expression<Func<T, TCollectionItem>> propertySelector)
        where TCollection : IReadOnlyCollection<TCollectionItem>
    {
        ArgumentNullException.ThrowIfNull(propertySelector);
        ArgumentNullException.ThrowIfNull(query);

        if (collection is not { Count: > 0 })
        {
            return query;
        }

        Expression<Func<T, bool>> expression = t => collection.Contains(propertySelector.Invoke(t));

        return query.Where(expression.Expand());
    }

    /// <summary>
    /// Применяет нечувствительный к регистру фильтр поиска по полою с частичным совпадением.
    /// </summary>
    /// <param name="query">Запрос.</param>
    /// <param name="searchText">Текст для поиска.</param>
    /// <param name="propertySelectors">Выражение выбора свойств, к которым применять поиск.</param>
    /// <typeparam name="T">Тип сущности.</typeparam>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="query" /> или <paramref name="propertySelectors" /> равен <c>null</c>.
    /// </exception>
    // TODO Доработать метод, чтобы можно было использовать его как Include().ThenInclude()
    public static IQueryable<T> ApplyContainsFilter<T>(this IQueryable<T> query, string? searchText, params Expression<Func<T, string?>>[] propertySelectors)
    {
        ArgumentNullException.ThrowIfNull(propertySelectors);
        ArgumentNullException.ThrowIfNull(query);

        if (string.IsNullOrWhiteSpace(searchText) || propertySelectors.Length == 0)
        {
            return query;
        }

        Expression<Func<T, bool>> expression = t => false;

        expression = propertySelectors.Aggregate(expression,
                                                 (current, propertySelector) => current.Or(t => propertySelector.Invoke(t) != null &&
                                                                                                propertySelector.Invoke(t)!.ToLower()
                                                                                                                .Contains(searchText.ToLower())));

        return query.Where(expression.Expand());
    }

    /// <summary>
    /// Применяет фильтр.
    /// </summary>
    /// <param name="query">Запрос.</param>
    /// <param name="apply">Признак применения фильтра.</param>
    /// <param name="filter">Выражение фильтра.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="query" /> или <paramref name="filter" /> равен <c>null</c>.
    /// </exception>
    public static IQueryable<T> ApplyFilter<T>(this IQueryable<T> query, bool apply, Expression<Func<T, bool>> filter)
    {
        ArgumentNullException.ThrowIfNull(filter);
        ArgumentNullException.ThrowIfNull(query);
        return apply ? query.Where(filter) : query;
    }

    /// <summary>
    /// Применяет фильтр "учитывать удаленные".
    /// </summary>
    /// <param name="query">Запрос.</param>
    /// <param name="apply">Признак применения фильтра.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="query" /> равен <c>null</c>.
    /// </exception>
    public static IQueryable<T> ApplyIncludeDeleted<T>(this IQueryable<T> query, bool apply) where T : class
    {
        ArgumentNullException.ThrowIfNull(query);
        return apply ? query.IgnoreQueryFilters() : query;
    }

    /// <summary>
    /// Применяет фильтр, если аргумент не равен <c>null</c>.
    /// </summary>
    /// <param name="query">Запрос.</param>
    /// <param name="argument">Аргумент фильтра.</param>
    /// <param name="filter">Выражение фильтра.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="query" /> или <paramref name="filter" /> равен <c>null</c>.
    /// </exception>
    public static IQueryable<T> ApplyMatchesFilter<T, TValue>(this IQueryable<T> query, TValue? argument, Expression<Func<T, TValue, bool>> filter)
    {
        ArgumentNullException.ThrowIfNull(query);
        ArgumentNullException.ThrowIfNull(filter);

        if (argument is not { } value)
        {
            return query;
        }

        Expression<Func<T, bool>> expression = t => filter.Invoke(t, value);
        return query.Where(expression.Expand());
    }

    /// <summary>
    /// Применяет фильтр, если аргумент не равен <c>null</c>.
    /// </summary>
    /// <param name="query">Запрос.</param>
    /// <param name="argument">Аргумент фильтра.</param>
    /// <param name="propertySelector">Выражение выбора значения для поиска.</param>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="query" /> или <paramref name="propertySelector" /> равен <c>null</c>.
    /// </exception>
    public static IQueryable<T> ApplyMatchesFilter<T, TValue>(this IQueryable<T> query, TValue? argument, Expression<Func<T, TValue>> propertySelector)
    {
        ArgumentNullException.ThrowIfNull(query);
        ArgumentNullException.ThrowIfNull(propertySelector);

        if (argument is not { } value)
        {
            return query;
        }

        Expression<Func<T, bool>> expression = t => value.Equals(propertySelector.Invoke(t));
        return query.Where(expression.Expand());
    }

    /// <summary>
    /// Применяет пагинацию.
    /// </summary>
    /// <param name="query">Запрос.</param>
    /// <param name="skip">Смещение для пагинации.</param>
    /// <param name="take">Количество запрашиваемых сущностей.</param>
    /// <typeparam name="T">Тип сущности.</typeparam>
    /// <exception cref="ArgumentNullException">
    /// Возникает, если <paramref name="query" /> равен <c>null</c>.
    /// </exception>
    /// <remarks>
    /// Необходимо использовать последним в цепочке методов.
    /// </remarks>
    public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> query, int? skip, int? take)
    {
        ArgumentNullException.ThrowIfNull(query);

        return query.HandleSkip(skip)
                    .HandleTake(take);
    }

    private static IQueryable<T> HandleSkip<T>(this IQueryable<T> query, int? skip) =>
        skip switch
        {
            null or 0 => query,
            > 0 => query.Skip(skip.Value),
            < 0 => throw new ArgumentOutOfRangeException(nameof(skip), "Значение не может быть меньше нуля.")
        };

    private static IQueryable<T> HandleTake<T>(this IQueryable<T> query, int? take) =>
        take switch
        {
            null => query,
            >= 0 => query.Take(take.Value),
            < 0 => throw new ArgumentOutOfRangeException(nameof(take), "Значение не может быть меньше нуля.")
        };
}
