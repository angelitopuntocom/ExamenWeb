using MiApp.Domain.Entities;
using MiApp.Domain.Enums;

namespace MiApp.Domain.Interfaces;

public interface ITicketZoneRepository
{
    Task<IEnumerable<TicketZone>> GetByEventIdAsync(int eventId, CancellationToken ct = default);
    Task<TicketZone?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<TicketZone?> GetByEventAndTypeAsync(int eventId, ZoneType zoneType, CancellationToken ct = default);
    Task AddAsync(TicketZone zone, CancellationToken ct = default);
    void Update(TicketZone zone);
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
