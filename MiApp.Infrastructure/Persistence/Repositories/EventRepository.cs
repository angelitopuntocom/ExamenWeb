using MiApp.Domain.Entities;
using MiApp.Domain.Interfaces;
using MiApp.Domain.Enums;
using MiApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MiApp.Infrastructure.Persistence.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationDbContext _context;
        public EventRepository(ApplicationDbContext context) => _context = context;

        public async Task<IEnumerable<Event>> GetAllAsync(CancellationToken ct = default) => 
            await _context.Events.ToListAsync(ct);

        public async Task<IEnumerable<Event>> GetActiveAsync(CancellationToken ct = default) => 
            await _context.Events.Where(e => e.Status == EventStatus.Active).ToListAsync(ct);

        public async Task<Event?> GetByIdAsync(int id, CancellationToken ct = default) => 
            await _context.Events.FindAsync(new object[] { id }, ct);

        public async Task<Event?> GetByIdWithDetailsAsync(int id, CancellationToken ct = default) => 
            await _context.Events.Include(e => e.Zones).FirstOrDefaultAsync(e => e.Id == id, ct);

        public async Task AddAsync(Event ev, CancellationToken ct = default) => 
            await _context.Events.AddAsync(ev, ct);

        public void Update(Event ev) => 
            _context.Events.Update(ev);

        public async Task<int> SaveChangesAsync(CancellationToken ct = default) => 
            await _context.SaveChangesAsync(ct);
    }
}