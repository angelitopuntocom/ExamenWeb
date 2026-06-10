using MiApp.Domain.Enums;
namespace MiApp.Domain.Entities;
public class Event
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public DateTime Date { get; private set; }
    public string Place { get; private set; } = string.Empty;
    public EventStatus Status { get; private set; } = EventStatus.Active;
    private readonly List<TicketZone> _zones = new();
    public IReadOnlyCollection<TicketZone> Zones => _zones.AsReadOnly();
    private Event() { }
    public static Event Create(string name, string description, DateTime date, string place)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Nombre obligatorio.");
        if (string.IsNullOrWhiteSpace(place)) throw new ArgumentException("Lugar obligatorio.");
        return new Event { Name = name.Trim(), Description = description?.Trim() ?? "", Date = date, Place = place.Trim(), Status = EventStatus.Active };
    }
    public void Update(string name, string description, DateTime date, string place)
    {
        if (Status == EventStatus.Canceled) throw new InvalidOperationException("No se puede editar un evento cancelado.");
        Name = name.Trim(); Description = description?.Trim() ?? ""; Date = date; Place = place.Trim();
    }
    public void Cancel()
    {
        if (Status == EventStatus.Canceled) throw new InvalidOperationException("Ya está cancelado.");
        Status = EventStatus.Canceled;
    }
    public void AddZone(TicketZone zone)
    {
        if (_zones.Any(z => z.ZoneType == zone.ZoneType)) throw new InvalidOperationException($"Ya existe zona {zone.ZoneType}.");
        _zones.Add(zone);
    }
}
