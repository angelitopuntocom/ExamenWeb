using MiApp.Domain.Entities;
using MiApp.Domain.Interfaces;
using MiApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MiApp.Infrastructure.Persistence.Repositories
{
    public class TicketPurchaseRepository : ITicketPurchaseRepository
    {
        private readonly ApplicationDbContext _context;
        public TicketPurchaseRepository(ApplicationDbContext context) => _context = context;

        public async Task<IEnumerable<TicketPurchase>> GetByEventIdAsync(int eventId, CancellationToken ct = default) => 
            await _context.TicketPurchases.Where(tp => tp.EventId == eventId).ToListAsync(ct);

        public async Task<TicketPurchase?> GetByIdAsync(int id, CancellationToken ct = default) => 
            await _context.TicketPurchases.FindAsync(new object[] { id }, ct);

        public async Task<int> CountSoldByZoneAsync(int zoneId, CancellationToken ct = default) => 
            await _context.TicketPurchases.Where(tp => tp.ZoneId == zoneId).SumAsync(tp => tp.Quantity, ct);

        public async Task AddAsync(TicketPurchase purchase, CancellationToken ct = default) => 
            await _context.TicketPurchases.AddAsync(purchase, ct);

        public async Task<int> SaveChangesAsync(CancellationToken ct = default) => 
            await _context.SaveChangesAsync(ct);
    }
}