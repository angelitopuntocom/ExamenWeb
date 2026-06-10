let allEvents = [];
let editingId = null;

async function loadEvents() {
  allEvents = await EventAPI.getAllAdmin();
  renderTable(allEvents);
}

function renderTable(events) {
  const tbody = document.getElementById("eventsTableBody");
  if (events.length === 0) {
    tbody.innerHTML = '<tr><td colspan="6" style="text-align:center;color:#999;">No hay eventos registrados.</td></tr>';
    return;
  }
  tbody.innerHTML = events.map(ev => {
    const dateStr = new Date(ev.date).toLocaleDateString("es-MX");
    const statusBadge = ev.status === "Active"
      ? '<span class="badge active">Activo</span>'
      : '<span class="badge cancelled">Cancelado</span>';
    return `
      <tr>
        <td>${ev.id}</td>
        <td>${ev.name}</td>
        <td>${dateStr}</td>
        <td>${ev.location}</td>
        <td>${statusBadge}</td>
        <td>
          <button class="btn-edit" onclick="openEdit(${ev.id})">✏️ Editar</button>
          <button class="btn-cancel" onclick="cancelEvent(${ev.id})" ${ev.status === 'Cancelled' ? 'disabled' : ''}>🚫 Cancelar</button>
        </td>
      </tr>
    `;
  }).join("");
}

function filterTable() {
  const query = document.getElementById("searchAdmin").value.toLowerCase();
  const filtered = allEvents.filter(ev =>
    ev.name.toLowerCase().includes(query) ||
    ev.location.toLowerCase().includes(query)
  );
  renderTable(filtered);
}

function openCreate() {
  editingId = null;
  document.getElementById("modalTitle").textContent = "Nuevo Evento";
  document.getElementById("eventForm").reset();
  document.getElementById("formModal").classList.add("active");
}

function openEdit(id) {
  editingId = id;
  const ev = allEvents.find(e => e.id === id);
  if (!ev) return;

  document.getElementById("modalTitle").textContent = "Editar Evento";
  document.getElementById("fieldName").value = ev.name;
  document.getElementById("fieldDesc").value = ev.description;
  document.getElementById("fieldDate").value = ev.date.substring(0, 16);
  document.getElementById("fieldLocation").value = ev.location;
  document.getElementById("formModal").classList.add("active");
}

function closeFormModal() {
  document.getElementById("formModal").classList.remove("active");
  editingId = null;
}

async function saveEvent() {
  const data = {
    name: document.getElementById("fieldName").value.trim(),
    description: document.getElementById("fieldDesc").value.trim(),
    date: document.getElementById("fieldDate").value,
    location: document.getElementById("fieldLocation").value.trim()
  };

  if (!data.name || !data.date || !data.location) {
    alert("Por favor llena todos los campos obligatorios.");
    return;
  }

  if (editingId) {
    await EventAPI.update(editingId, data);
  } else {
    await EventAPI.create(data);
  }

  closeFormModal();
  await loadEvents();
}

async function cancelEvent(id) {
  if (!confirm("¿Seguro que deseas cancelar este evento?")) return;
  await EventAPI.cancel(id);
  await loadEvents();
}

loadEvents();
