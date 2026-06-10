let allEvents = [];
let currentEvent = null;

async function loadEvents() {
  allEvents = await EventAPI.getAll();
  renderEvents(allEvents);
}

function renderEvents(events) {
  const grid = document.getElementById("eventsGrid");
  if (events.length === 0) {
    grid.innerHTML = '<p class="no-events">No se encontraron eventos.</p>';
    return;
  }
  grid.innerHTML = events.map(ev => {
    const minPrice = Math.min(...ev.zones.map(z => z.price));
    const dateStr = new Date(ev.date).toLocaleDateString("es-MX", {
      weekday: "long", year: "numeric", month: "long", day: "numeric"
    });
    return `
      <div class="event-card" onclick="openModal(${ev.id})">
        <h3>${ev.name}</h3>
        <p class="date">📅 ${dateStr}</p>
        <p class="location">📍 ${ev.location}</p>
        <p class="price-from">Desde $${minPrice.toLocaleString("es-MX")}</p>
        <button class="btn">Ver y Comprar</button>
      </div>
    `;
  }).join("");
}

function filterEvents() {
  const query = document.getElementById("searchInput").value.toLowerCase();
  const filtered = allEvents.filter(ev =>
    ev.name.toLowerCase().includes(query) ||
    ev.location.toLowerCase().includes(query)
  );
  renderEvents(filtered);
}

function openModal(id) {
  currentEvent = allEvents.find(e => e.id === id);
  if (!currentEvent) return;

  document.getElementById("modalTitle").textContent = currentEvent.name;
  document.getElementById("modalMeta").textContent =
    `📅 ${new Date(currentEvent.date).toLocaleDateString("es-MX")} · 📍 ${currentEvent.location}`;
  document.getElementById("modalDesc").textContent = currentEvent.description;

  const zoneSelect = document.getElementById("zoneSelect");
  zoneSelect.innerHTML = currentEvent.zones.map(z =>
    `<option value="${z.id}" data-price="${z.price}">${z.name} — $${z.price.toLocaleString("es-MX")}</option>`
  ).join("");

  document.getElementById("quantityInput").value = 1;
  document.getElementById("successMsg").style.display = "none";
  updateTotal();
  document.getElementById("modalOverlay").classList.add("active");
}

function closeModal() {
  document.getElementById("modalOverlay").classList.remove("active");
  currentEvent = null;
}

function updateTotal() {
  const zoneSelect = document.getElementById("zoneSelect");
  const selected = zoneSelect.options[zoneSelect.selectedIndex];
  const price = parseInt(selected?.dataset.price || 0);
  const qty = parseInt(document.getElementById("quantityInput").value || 1);
  document.getElementById("totalDisplay").textContent =
    `Total: $${(price * qty).toLocaleString("es-MX")}`;
}

async function confirmPurchase() {
  const zoneId = parseInt(document.getElementById("zoneSelect").value);
  const qty = parseInt(doc
