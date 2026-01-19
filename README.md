# Постепенный разбор работы с kafka в dotnet (.NET 8)

- **Module-2-2** - стартовая точка, базовый пример продюсера и консьюмера.  
```bash
dotnet run --project Module-2-2/Module-2-2.csproj -- consumer 
dotnet run --project Module-2-2/Module-2-2.csproj -- producer
```

<br/><br/>

- **Module-2-4** — работа с группами консьюмеров и партициями.  
  Запускаю 2 консьюмера и 1 продюсера:

  ```bash
  dotnet run --project Module-2-4/Module-2-4.csproj -- consumer
  dotnet run --project Module-2-4/Module-2-4.csproj -- consumer
  dotnet run --project Module-2-4/Module-2-4.csproj -- producer
  ```
