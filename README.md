Ensure docker is start

```
Start database: docker compose up -d
Stop database: docker compose down
View DB logs: docker compose logs postgres_db -f
```

Run application
```
dotnet run --project e_commerce_web
Hot Reload: dotnet watch --project e_commerce_web
```

Navigate to http://localhost:5068
