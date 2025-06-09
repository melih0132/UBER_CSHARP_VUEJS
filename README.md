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

* **Back-end**: 
  * .NET 8
  * Entity Framework Core
  * RESTful Web API
  * xUnit for testing
* **Front-end**: 
  * Vue.js 3
  * Vite
  * Pinia (state management)
  * Vue Router
  * Axios
* **Database**: PostgreSQL
* **Testing**: 
  * xUnit for back-end (.NET)
  * Unit testing for front-end (Vue)

---

## Prerequisites

* [.NET 8 SDK](https://dotnet.microsoft.com/download)
* [Node.js (>=16.x)](https://nodejs.org/)
* [PostgreSQL](https://www.postgresql.org/)
* [Vue CLI / Vite](https://vitejs.dev/)

---

## Installation

### 1. Clone the repository

```bash
git clone https://github.com/melih0132/uber-clone-dotnet-vue.git
cd uber-clone-dotnet-vue
```

### 2. Start the API (.NET)

```bash
cd UberApi
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

### Environment Variables

Create a `.env` file in the front-end project root to store API URLs and tokens:

```env
VITE_API_URL=http://localhost:5000/api
```

### Database Configuration

Configure your PostgreSQL connection in `UberApi/appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=uber_clone;Username=your_username;Password=your_password"
  }
}
```

---

## Project Structure

```
uber-clone-dotnet-vue/
├── UberApi/                    → .NET Back-end API
│   ├── UberApi/               → Main project
│   │   ├── Controllers/       → REST Controllers
│   │   ├── Models/           → Entities and models
│   │   ├── Services/         → Business services
│   │   ├── Data/             → EF Core context
│   │   └── Migrations/       → EF Core migrations
│   └── UberApiTests/         → Unit tests
│
└── UberVueJS/                 → Vue.js Front-end application
    ├── src/
    │   ├── components/       → Reusable UI components
    │   ├── views/           → Main pages
    │   ├── stores/          → Pinia stores
    │   ├── services/        → API services
    │   ├── assets/          → Static resources
    │   └── router/          → Router configuration
    ├── tests/               → Unit tests
    └── public/              → Public files
```

---

## Best Practices

* Use of **TypeScript** for front-end development
* Unit tests for services and controllers
* Security: role management, token expiration, data validation
* Modular architecture: separated services, MVC architecture on the back-end
* Code documentation with XML comments
* Git Flow for version management

---

## Contributing

Contributions are welcome:

1. Fork the project
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

---

## University Project

This project was developed as part of the 4th semester of the Computer Science BUT program.

## License

This project is licensed under the MIT License. See the `LICENSE` file for details.
