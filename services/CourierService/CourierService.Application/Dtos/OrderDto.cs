namespace CourierService.Application.Dtos;

/// <summary>
/// Представляет заказ.
/// </summary>
public sealed record OrderDto
{
    /// <summary>
    /// Возвращает наименование цели.
    /// </summary>
    public decimal DeliveryCost { get; init; }

    /// <summary>
    /// Возвращает наименование цели.
    /// </summary>
    public required OrderStatusCodeEnum OrderStatus { get; init; }

    /// <summary>
    /// Возвращает наименование цели.
    /// </summary>
    public required PaymentMethodCodeEnum PaymentMethod { get; init; }

    /// <summary>
    /// Возвращает наименование цели.
    /// </summary>
    public required decimal ProductCost { get; init; }

    /// <summary>
    /// Возвращает наименование цели.
    /// </summary>
    public required string ProductDescription { get; init; }

    /// <summary>
    /// Возвращает наименование цели.
    /// </summary>
    public required int ProductWeight { get; init; }

    /// <summary>
    /// Возвращает наименование цели.
    /// </summary>
    public required string ReceiverAddress { get; init; }

    /// <summary>
    /// Возвращает наименование цели.
    /// </summary>
    public required string ReceiverName { get; init; }

    /// <summary>
    /// Возвращает наименование цели.
    /// </summary>
    public required string SenderAddress { get; init; }

    /// <summary>
    /// Возвращает наименование цели.
    /// </summary>
    public required string SenderName { get; init; }

    /// <summary>
    /// Возвращает наименование цели.
    /// </summary>
    public required Guid TrackNumber { get; init; }
}
