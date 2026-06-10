using MiApp.Domain.Enums;
namespace MiApp.Domain.Entities;
public class TicketZone
{
    public int Id { get; private set; }
    public int EventId { get; private set; }
    public ZoneType ZoneType { get; private set; }
    public decimal Price { get; private set; }
    public int? Capacity { get; private set; }
    public Event? Event { get; private set; }
    private readonly List<TicketPurchase> _purchases = new();
    public IReadOnlyCollection<TicketPurchase> Purchases => _purchases.AsReadOnly();
    private TicketZone() { }
    public static TicketZone Create(int eventId, ZoneType zoneType, decimal price, int? capacity = null)
    {
        if (price < 0) throw new ArgumentException("Precio no puede ser negativo.");
        return new TicketZone { EventId = eventId, ZoneType = zoneType, Price = price, Capacity = capacity };
    }
    public void UpdatePricing(decimal newPrice, int? newCapacity) { Price = newPrice; Capacity = newCapacity; }
    public bool HasAvailability(int qty)
    {
        if (!Capacity.HasValue) return true;
        int sold = _purchases.Sum(p => p.Quantity);
        return (sold + qty) <= Capacity.Value;
    }
}
