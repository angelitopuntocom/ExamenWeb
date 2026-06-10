using MiApp.Domain.Entities;

namespace MiApp.Domain.Interfaces;

public interface IEventRepository
{
    Task<IEnumerable<Event>> GetAllAsync(CancellationToken ct = default);
    Task<IEnumerable<Event>> GetActiveAsync(CancellationToken ct = default);
    Task<Event?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<Event?> GetByIdWithDetailsAsync(int id, CancellationToken ct = default);
    Task AddAsync(Event ev, CancellationToken ct = default);
    void Update(Event ev);
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
