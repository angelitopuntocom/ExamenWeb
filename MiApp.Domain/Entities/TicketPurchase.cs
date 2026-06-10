namespace MiApp.Domain.Entities;
public class TicketPurchase
{
    public int Id { get; private set; }
    public int EventId { get; private set; }
    public int ZoneId { get; private set; }
    public string BuyerName { get; private set; } = string.Empty;
    public string BuyerEmail { get; private set; } = string.Empty;
    public int Quantity { get; private set; }
    public decimal TotalPrice { get; private set; }
    public DateTime PurchasedAt { get; private set; }
    public Event? Event { get; private set; }
    public TicketZone? Zone { get; private set; }
    private TicketPurchase() { }
    public static TicketPurchase Create(int eventId, int zoneId, string buyerName, string buyerEmail, int quantity, decimal unitPrice)
    {
        if (string.IsNullOrWhiteSpace(buyerName)) throw new ArgumentException("Nombre obligatorio.");
        if (string.IsNullOrWhiteSpace(buyerEmail)) throw new ArgumentException("Correo obligatorio.");
        if (quantity <= 0) throw new ArgumentException("Cantidad debe ser mayor a cero.");
        return new TicketPurchase { EventId = eventId, ZoneId = zoneId, BuyerName = buyerName.Trim(), BuyerEmail = buyerEmail.Trim().ToLowerInvariant(), Quantity = quantity, TotalPrice = unitPrice * quantity, PurchasedAt = DateTime.UtcNow };
    }
}
