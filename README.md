# 🧩 Reactive Microservices with RabbitMQ using ASP.NET Core (.NET 8)
This project demonstrates:

Event-driven communication
Asynchronous processing
Loose coupling
Independent scalability
Message-based architecture
Reactive service interaction

This project is designed as a learning + portfolio backend service to showcase real-world backend engineering practices.

## 🚀 Features

* ASP.NET Core Web API
* RabbitMQ
* Background Worker Service
* Dependency Injection
* Async messaging with RabbitMQ.Client 7.2.1
* Shared message contracts
* Clean project separation inside a single solution

The solution contains:
ReactiveMicroservices.sln
│
├── Producer.API
├── Consumer.Worker
├── Shared.Messages
└── Infrastructure.Messaging

## 🏗️ Architecture

```text
+------------------+
|   Producer.API   |
+------------------+
          |
          | Publish Event
          v
+------------------+
|     RabbitMQ     |
|   order-queue    |
+------------------+
          |
          | Consume Event
          v
+--------------------+
| Consumer.Worker    |
+--------------------+
```

The producer publishes events to RabbitMQ.
The consumer listens asynchronously and processes events independently.

## 🛠️ Tech Stack

* ASP.NET Core (.NET 8)	Web API and Worker Services
* RabbitMQ Message Broker
* RabbitMQ.Client 7.2.1	RabbitMQ .NET Client
* Dependency Injection Service registration
* BackgroundService Long-running consumer

## ⚙️ Running Locally

### 1. Clone repository
```
git clone https://github.com/NagendraManerikar/ReactiveMicroservices-using-RabbitMQ
```

### 2. Start RabbitMQ
```
run following commands in cmd propmt
cd C:\Program Files\RabbitMQ Server\rabbitmq_server-4.3.0\sbin
rabbitmq-plugins enable rabbitmq_management
browse and login to rabbitMq (guest/guest)
```

### 3. Run Producer.API
```
dotnet run
```

### 4. Run Consumer.Worker
```
dotnet run
```

### 5. Call API endpoint:
```
POST /api/orders
post the request using swagger
```

### 6. Consumer receives and processes event
```
Observe the queue being created and processed in RabbitMq
```

## 👨‍💻 Author

Passionate about building scalable backend systems using ASP.NET Core using microservice architecture.

## 📜 License

MIT License — Free to use for learning purposes.



