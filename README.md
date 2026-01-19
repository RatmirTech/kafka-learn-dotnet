# Постепенный разбор работы с kafka в dotnet (.NET 8)

- **Module-2-2** - стартовая точка, базовый пример продюсера и консьюмера.  
```bash
dotnet run --project Module-2-2/Module-2-2.csproj -- consumer 
dotnet run --project Module-2-2/Module-2-2.csproj -- producer
```

- **Module-2-4** — работа с группами консьюмеров и партициями.  
  Запускаю 2 консьюмера и 1 продюсера:

  ```bash
  dotnet run --project Module-2-4/Module-2-4.csproj -- consumer
  dotnet run --project Module-2-4/Module-2-4.csproj -- consumer
  dotnet run --project Module-2-4/Module-2-4.csproj -- producer
  ```

# Docker compose
```bash
services:
  zookeeper:
    image: confluentinc/cp-zookeeper:7.0.1
    environment:
      ZOOKEEPER_CLIENT_PORT: 2181
      ZOOKEEPER_TICK_TIME: 2000
    ports:
      - "2181:2181"
  kafka:
    image: confluentinc/cp-kafka:7.0.1
    depends_on:
      - zookeeper
    environment:
      KAFKA_BROKER_ID: 1
      KAFKA_ZOOKEEPER_CONNECT: zookeeper:2181
      KAFKA_ADVERTISED_LISTENERS: PLAINTEXT://localhost:9092
      KAFKA_OFFSETS_TOPIC_REPLICATION_FACTOR: 1
    ports:
      - "9092:9092"

  schema-registry:
    image: confluentinc/cp-schema-registry:latest
    depends_on:
      - kafka
    ports:
      - "8081:8081"
    environment:
      SCHEMA_REGISTRY_KAFKASTORE_BOOTSTRAP_SERVERS: PLAINTEXT://kafka:9092
      SCHEMA_REGISTRY_HOST_NAME: schema-registry
      SCHEMA_REGISTRY_LISTENERS: http://0.0.0.0:8081

  kafka-ui:
    image: provectuslabs/kafka-ui
    depends_on:
      - kafka
    ports:
      - "8080:8080"
    environment:
      KAFKA_CLUSTERS_0_NAME: local
      KAFKA_CLUSTERS_0_BOOTSTRAPSERVERS: kafka:9092
      KAFKA_CLUSTERS_0_ZOOKEEPER: zookeeper:2181
```
