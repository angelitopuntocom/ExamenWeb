using MiApp.Domain.Entities;
namespace MiApp.Domain.Interfaces;
public interface ITicketPurchaseRepository
{
    Task<IEnumerable<TicketPurchase>> GetByEventIdAsync(int eventId, CancellationToken ct = default);
    Task<TicketPurchase?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<int> CountSoldByZoneAsync(int zoneId, CancellationToken ct = default);
    Task AddAsync(TicketPurchase purchase, CancellationToken ct = default);
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
