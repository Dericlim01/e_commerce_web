# Lumière E-Commerce Web

A modern E-Commerce web application built with ASP.NET Core MVC and PostgreSQL.

## Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Docker & Docker Compose](https://www.docker.com/products/docker-desktop)

---

## 🚀 Running the Application

There are two ways to run the application locally depending on your development workflow.

### Option 1: Full Docker Setup (Recommended)
This runs both the web application and the PostgreSQL database in isolated Docker containers.

```bash
docker-compose up --build
```
- **Web App:** 👉 [http://localhost:8080](http://localhost:8080)
- **Database:** Runs in the background on port `5432`.

### Option 2: Local .NET + Docker Database
This runs the web application natively on your machine (allowing for Hot Reload) while keeping the database in Docker.

1. Start just the database:
   ```bash
   docker compose up -d postgres_db
   ```
2. Run the web application (with Hot Reload):
   ```bash
   dotnet watch --project e_commerce_web
   ```
   *(Or use `dotnet run --project e_commerce_web` for standard execution)*
- **Web App:** 👉 [http://localhost:5068](http://localhost:5068)

---

## 🗄️ Database Management

When running the application, Entity Framework Core will automatically apply pending migrations and create the schema on startup. 

**Accessing the Docker Database via Terminal (`psql`):**
```bash
docker exec -it ecommerce_postgres psql -U postgres -d ecommerce_db
```

**Common Database Commands:**
- Stop database: `docker compose down`
- View DB logs: `docker compose logs postgres_db -f`
- Manual initialization (if running native Postgres instead of Docker):
  ```bash
  createdb -U postgres ecommerce_db
  ```
