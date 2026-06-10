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
  getAllAdmin: async () => {
    if (USE_MOCK) return mockEvents;
    const res = await fetch(`${API_BASE}/events`);
    return res.json();
  },

  create: async (data) => {
    if (USE_MOCK) {
      const newEvent = { ...data, id: mockEvents.length + 1, status: "Active", zones: [] };
      mockEvents.push(newEvent);
      return newEvent;
    }
    const res = await fetch(`${API_BASE}/events`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(data)
    });
    return res.json();
  },

  update: async (id, data) => {
    if (USE_MOCK) {
      const idx = mockEvents.findIndex(e => e.id === id);
      if (idx !== -1) mockEvents[idx] = { ...mockEvents[idx], ...data };
      return mockEvents[idx];
    }
    const res = await fetch(`${API_BASE}/events/${id}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(data)
    });
    return res.json();
  },

  cancel: async (id) => {
    if (USE_MOCK) {
      const ev = mockEvents.find(e => e.id === id);
      if (ev) ev.status = "Cancelled";
      return ev;
    }
    const res = await fetch(`${API_BASE}/events/${id}/cancel`, { method: "PATCH" });
    return res.json();
  }
};
