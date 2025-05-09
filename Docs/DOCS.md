# Документация сервиса "Табло"

## 1. Краткое описание

Сервис **"Табло"** предназначен для управления рейсами и взаимодействия с другими сервисами аэропорта. Основные функции:

1. **Создание рейса** (**Flight**) на основе переданного **самолета** (**Airplane**) от сервиса "Самолет".
2. **Уведомление сервисов "Пассажир" и "Касса"** о создании рейса.
3. **Оповещение сервисов "Пассажир" и "Касса"** о начале регистрации на рейс (по сигналу от сервиса "Управление наземным обслуживанием").
4. **Ожидание заданного интервала модельного времени** после начала регистрации.
5. **Уведомление сервисов "Пассажир" и "Касса"** о завершении регистрации.
6. **Предоставление текущего модельного времени**.
7. **Реализация методов управления модельным временем** (изменение параметра SecondsPerTick).

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

#### **Зависимости:**
- `DepartureBoard.Domain`

### **2.3 DepartureBoard.Infrastructure**
Проект, включающий:
- **Адаптеры (имплементации) портов** из `DepartureBoard.Application`;
- **DbContext** приложения и все его миграции.

#### **Зависимости:**
- `Npgsql.EntityFrameworkCore.PostgreSQL` **(9.0.4)** - поддержка PostgreSQL в Entity Framework Core.
- `DepartureBoard.Domain`
- `DepartureBoard.Application`

### **2.4 DepartureBoard.Api**
Этот проект реализует **API сервиса**, включая:
- Контроллеры;
- Middleware;
- Различные настройки и опции.

#### **Зависимости:**
- `Microsoft.EntityFrameworkCore.Design` **(9.0.3)** - инструменты для работы с EF Core.
- `Swashbuckle.AspNetCore` **(7.3.1)** - генерация Swagger-документации.
- `DepartureBoard.Domain`
- `DepartureBoard.Application`
- `DepartureBoard.Infrastructure`

### **2.5 DepartureBoard.Desktop**
Простое **WPF-приложение** для управления модельным временем.

![DesktopApp](https://github.com/OOO-Aeroport/departure-board/blob/master/Docs/Images/img7.jpg)

Не забудьте изменить базовый адрес SingletonHttpClient для связи с сервисом "Табло" в **DepartureBoardBaseAddress.txt**.

## 3. Взаимодействие с другими сервисами

### **Создание рейса**
1. **Сервис "Самолет"** отправляет информацию о самолете.
2. **Сервис "Табло"** создает рейс и отправляет уведомления сервисам "Пассажир" и "Касса".

### **Регистрация пассажиров**
1. **Сервис "Управление наземным обслуживанием"** отправляет сигнал о начале регистрации.
2. **Сервис "Табло"** пересылает уведомление сервисам "Пассажир" и "Касса".
3. **Проходит установленный интервал модельного времени**.
4. **Сервис "Табло"** уведомляет сервисы "Пассажир" и "Касса" о завершении регистрации.

Все базовые адреса внешних сервисов расположены и доступны для изменения в appsettings.json:
```json
"Clients": {
    "TicketOfficeHttpClient":
    {
      "BaseAddress": "http://26.109.26.0:5555/ticket-office/"
    },
    "PassengerHttpClient":
    {
      "BaseAddress": "http://26.49.89.37:5555/passenger/"
    },
    "CheckInHttpClient":
    {
      "BaseAddress": "http://26.109.26.0:5555/check-in/"
    }
  }
```

## 4. API

[Документация API](https://github.com/OOO-Aeroport/departure-board/blob/master/README.md)

## 5. Настройки

### **Конфигурация подключения к БД**
Файл: `appsettings.json` (DepartureBoard.Api)
```json
{
  "ConnectionStrings": {
    "Postgres": "Host=localhost;Database=dep_board;Username=dep_board_admin;Password=samolet"
  }
}
```

Для создания актуальной базы данных **обновитесь до миграции "ReduceAirplane"** (ef database update).

## 6. Конфигурации запуска

### Запуск сервиса

Сервис "Табло" поддерживает две конфигурации запуска, предназначенные для различных сценариев работы:

1. Run (Обычный запуск)

Запускает сервис в стандартном режиме без удаления данных.

2. Reboot (Перезапуск с очисткой БД)

При старте:
- Очищает базу данных (TRUNCATE всех записей).
- Мгновенно завершает работу программы после очистки.

### Запуск настольного приложения

Для запуска WPF-приложения используйте конфигурацию **DepartureBoard.Desktop**.
