const mockEvents = [
  {
    id: 1,
    name: "Concierto Rock en Vivo",
    description: "La mejor noche de rock del año con bandas locales e internacionales.",
    date: "2025-08-15T20:00:00",
    location: "Foro Sol, Ciudad de México",
    status: "Active",
    zones: [
      { id: 1, name: "VIP", price: 1500, available: 50 },
      { id: 2, name: "Preferente", price: 800, available: 120 },
      { id: 3, name: "General", price: 350, available: 500 }
    ]
  },
  {
    id: 2,
    name: "Conferencia de Tecnología 2025",
    description: "Speakers internacionales hablarán sobre IA, cloud y desarrollo web.",
    date: "2025-09-10T09:00:00",
    location: "Centro Citibanamex, CDMX",
    status: "Active",
    zones: [
      { id: 4, name: "VIP", price: 2000, available: 30 },
      { id: 5, name: "Preferente", price: 1000, available: 80 },
      { id: 6, name: "General", price: 500, available: 300 }
    ]
  },
  {
    id: 3,
    name: "Festival de Jazz",
    description: "Tres días de jazz en vivo al aire libre.",
    date: "2025-10-05T18:00:00",
    location: "Parque Bicentenario, Querétaro",
    status: "Active",
    zones: [
      { id: 7, name: "VIP", price: 1200, available: 40 },
      { id: 8, name: "Preferente", price: 600, available: 100 },
      { id: 9, name: "General", price: 250, available: 400 }
    ]
  }
];
