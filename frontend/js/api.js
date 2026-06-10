// Cuando la API de Val esté lista, cambia USE_MOCK a false
const USE_MOCK = true;
const API_BASE = "http://localhost:5000/api";

const EventAPI = {
  getAll: async () => {
    if (USE_MOCK) return mockEvents.filter(e => e.status === "Active");
    const res = await fetch(`${API_BASE}/events`);
    return res.json();
  },

  getById: async (id) => {
    if (USE_MOCK) return mockEvents.find(e => e.id === id);
    const res = await fetch(`${API_BASE}/events/${id}`);
    return res.json();
  },

  purchase: async (eventId, zoneId, quantity) => {
    if (USE_MOCK) {
      const event = mockEvents.find(e => e.id === eventId);
      const zone = event?.zones.find(z => z.id === zoneId);
      return { success: true, total: zone.price * quantity };
    }
    const res = await fetch(`${API_BASE}/purchases`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ eventId, zoneId, quantity })
    });
    return res.json();
  }
};
