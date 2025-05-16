# Uber – Full Stack Application (.NET Core & Vue.js)

## Overview

A full stack web application inspired by the Uber model, developed with **.NET Core C#** for the back-end and **Vue.js** for the front-end. The project supports order management, meal delivery (Uber Eats), ride booking, and includes secure authentication using JWT.

---

## Key Features

* JWT authentication (registration, login, account management)
* Ride booking with trip history
* Meal ordering and delivery (Uber Eats)
* Management of restaurants, products, and payment methods
* Interactive map with Leaflet
* Responsive UI built with Vue.js

---

## Technologies Used

* **Back-end**: .NET 8, Entity Framework Core, RESTful Web API
* **Front-end**: Vue.js 3, Vite, Pinia, Vue Router, Axios
* **Database**: PostgreSQL
* **Testing**: xUnit (.NET) & JS Testing (Vue)

---

## Prerequisites

* [.NET 6 SDK](https://dotnet.microsoft.com/download)
* [Node.js (>=16.x)](https://nodejs.org/)
* [PostgreSQL](https://www.postgresql.org/)
* [Vue CLI / Vite](https://vitejs.dev/)

---

## Installation

### 1. Clone the repository

```bash
git clone https://github.com/melih0132/UBER_S4.git
```

### 2. Start the API (.NET)

```bash
cd UBER_S4/UberApi
dotnet restore
dotnet ef database update
dotnet run
```

Make sure to configure the connection strings in `appsettings.Development.json` if needed.

### 3. Start the front-end (Vue.js)

```bash
cd ../UberVueJS
npm install
npm run dev
```

---

## Configuration

### API Keys

Set Uber API keys or other service keys in a `config.js` file or via `process.env`.

### Environment Variables

Use a `.env` file to store API URLs and tokens:

```env
VITE_API_URL=http://localhost:5000/api
```

---

## Project Structure

### Simplified Directory Tree

```
UBER_S4/
├── UberApi/         → C# .NET REST API
│   ├── Controllers/ → REST Controllers
│   ├── Models/      → Entities + Data Managers
│   ├── Views/       → Razor MVC test/visualization pages
│   ├── Migrations/  → EF Core migrations
│   └── UberApiTests/→ Unit tests
└── UberVueJS/       → Vue.js front-end application
    ├── components/  → UI components (MapView, Navbar…)
    ├── views/       → Main views (Login, UberEats…)
    ├── services/    → API call services using Axios
    ├── stores/      → Pinia stores (user state, rides…)
    └── assets/      → Stylesheets, Leaflet JS files
```

---

## Best Practices

* **TypeScript** is recommended for future front-end development
* **Unit tests** for services and controllers
* **Security**: role management, token expiration, data validation
* **Modularity**: separated services, MVC architecture on the back-end

---

## Contribution

Contributions are welcome:

* Fork & submit a PR to propose improvements
* Open issues to report bugs or suggest features

---

## Group Project

University project for the 4th semester of the Computer Science BUT program.
