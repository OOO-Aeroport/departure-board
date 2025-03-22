# Документация сервиса "Табло"

## 1. Краткое описание

Сервис **"Табло"** предназначен для управления рейсами и взаимодействия с другими сервисами аэропорта. Основные функции:

1. **Создание рейса** (**Flight**) на основе переданного **самолета** (**Airplane**) от сервиса "Самолет".
2. **Уведомление сервисов "Пассажир" и "Касса"** о создании рейса.
3. **Оповещение сервисов "Пассажир" и "Касса"** о начале регистрации на рейс (по сигналу от сервиса "Управление наземным обслуживанием").
4. **Ожидание заданного интервала модельного времени** после начала регистрации.
5. **Уведомление сервисов "Пассажир" и "Касса"** о завершении регистрации.

## 2. Архитектура и зависимости

### Общая структура

Решение состоит из **5 проектов**, каждый из которых выполняет свою роль:

### **2.1 DepartureBoard.Domain**
Проект, определяющий **основные сущности** сервиса:
- **Airplane (Самолет)**
```
public class Airplane
{
    public required int Id { get; init; }
    [JsonPropertyName("baggage_available")]
    public required int BaggageAvailable { get; init; }
    [JsonPropertyName("seats_available")]
    public required int SeatsAvailable { get; init; }
    
    // Nav-prop
    [JsonIgnore]
    public Flight? Flight { get; init; }
}
```
- **Flight (Рейс)**
```
public class Flight
{
    public int Id { get; init; }
    public required int AirplaneId { get; init; }
    public required DateTime DepartureTime { get; init; }
    
    // Nav-prop
    [JsonIgnore]
    public Airplane? Airplane { get; init; }
}
```
- **Константы** для работы с модельным временем
```
public enum Constants
{
    TickInMs = 100,
    SecondsPerTick = 20,
    CheckInMinuteDuration = 180
}
```

### **2.2 DepartureBoard.Application**
Этот проект содержит:
- **Юз-кейсы** (сценарии использования сервиса);
- **Службу времени** для управления модельным временем;
- **Порты (абстракции)** для взаимодействия с базами данных и другими сервисами.

### **2.3 DepartureBoard.Infrastructure**
Проект, включающий:
- **Адаптеры (имплементации) портов** из `DepartureBoard.Application`;
- **DbContext** приложения и все его миграции.

#### **Зависимости:**
- `Npgsql.EntityFrameworkCore.PostgreSQL` **(9.0.4)** - поддержка PostgreSQL в Entity Framework Core.

### **2.4 DepartureBoard.Api**
Этот проект реализует **API сервиса**, включая:
- Контроллеры;
- Middleware;
- Различные настройки и опции.

#### **Зависимости:**
- `Microsoft.AspNetCore.OpenApi` **(9.0.0)** - поддержка OpenAPI.
- `Microsoft.EntityFrameworkCore.Design` **(9.0.3)** - инструменты для работы с EF Core.
- `Swashbuckle.AspNetCore` **(7.3.1)** - генерация Swagger-документации.

### **2.5 DepartureBoard.Desktop**
Простое **WPF-приложение** для управления модельным временем.

![DesktopApp](https://github.com/OOO-Aeroport/departure-board/blob/master/Docs/Images/img7.jpg)

## 3. Взаимодействие с другими сервисами

### **Создание рейса**
1. **Сервис "Самолет"** отправляет информацию о самолете.
2. **Сервис "Табло"** создает рейс и отправляет уведомления сервисам "Пассажир" и "Касса".

### **Регистрация пассажиров**
1. **Сервис "Управление наземным обслуживанием"** отправляет сигнал о начале регистрации.
2. **Сервис "Табло"** пересылает уведомление сервисам "Пассажир" и "Касса".
3. **Проходит установленный интервал модельного времени**.
4. **Сервис "Табло"** уведомляет сервисы "Пассажир" и "Касса" о завершении регистрации.

## 4. API

### **4.1 Создание рейса**
**`POST /api/flights`**  
Создает новый рейс на основе переданного самолета.

#### **Запрос:**
```json
{
  "airplaneId": "12345",
  "departureTime": "2025-04-10T14:30:00Z"
}
```

#### **Ответ:**
```json
{
  "flightId": "67890",
  "status": "Created"
}
```

### **4.2 Уведомление о начале регистрации**
**`POST /api/flights/{flightId}/check-in/start`**

### **4.3 Уведомление о завершении регистрации**
**`POST /api/flights/{flightId}/check-in/end`**

## 5. Настройки

### **Конфигурация подключения к БД**
Файл: `appsettings.json`
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=departure_board;Username=admin;Password=secret"
  }
}
```

---
Документация оформлена для удобства восприятия и содержит все ключевые аспекты работы сервиса "Табло".

