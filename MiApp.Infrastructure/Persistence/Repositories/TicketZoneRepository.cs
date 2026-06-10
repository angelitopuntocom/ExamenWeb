using MiApp.Domain.Entities;
using MiApp.Domain.Enums;
using MiApp.Domain.Interfaces;
using MiApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MiApp.Infrastructure.Persistence.Repositories
{
    public class TicketZoneRepository : ITicketZoneRepository
    {
        private readonly ApplicationDbContext _context;
        public TicketZoneRepository(ApplicationDbContext context) => _context = context;

        public async Task<IEnumerable<TicketZone>> GetByEventIdAsync(int eventId, CancellationToken ct = default) => 
            await _context.TicketZones.Where(z => z.EventId == eventId).ToListAsync(ct);

        public async Task<TicketZone?> GetByIdAsync(int id, CancellationToken ct = default) => 
            await _context.TicketZones.FindAsync(new object[] { id }, ct);

        public async Task<TicketZone?> GetByEventAndTypeAsync(int eventId, ZoneType zoneType, CancellationToken ct = default) => 
            await _context.TicketZones.FirstOrDefaultAsync(z => z.EventId == eventId && z.ZoneType == zoneType, ct);

        public async Task AddAsync(TicketZone zone, CancellationToken ct = default) => 
            await _context.TicketZones.AddAsync(zone, ct);

        public void Update(TicketZone zone) => 
            _context.TicketZones.Update(zone);

        public async Task<int> SaveChangesAsync(CancellationToken ct = default) => 
            await _context.SaveChangesAsync(ct);
    }
}