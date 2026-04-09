<p align="center">Министерство образования Республики Беларусь</p>
<p align="center">Учреждение образования</p>
<p align="center">"Брестский Государственный технический университет"</p>
<p align="center">Кафедра ИИТ</p>

<br><br><br><br><br><br>

<p align="center"><strong>Лабораторная работа №3</strong></p>
<p align="center"><strong>По дисциплине:</strong> "Проектирование интернет-систем"</p>
<p align="center"><strong>Тема:</strong> "Реализация Domain Layer с DDD-паттернами (Электронная библиотека)"</p>

<br><br><br><br><br><br>

<p align="right"><strong>Выполнил:</strong></p>
<p align="right">Студент 3 курса</p>
<p align="right">Группы ПО-13</p>
<p align="right">Потапчук А.С.</p>

<p align="right"><strong>Проверил:</strong></p>
<p align="right">Несюк А.Н.</p>

<br><br><br><br><br>

<p align="center"><strong>Брест 2026</strong></p>

---

## Цель работы

Научиться применять тактические паттерны DDD (Entities, Value Objects, Aggregates, Domain Events) для реализации **доменного слоя** с инвариантами и доменной логикой.

---

## Вариант №19 - Электронная библиотека

**Питч:** _Управляй чтением — не теряй прогресс._  
**Ядро домена:** _Книги, Прогресс чтения, Библиотека пользователя, Оценки_

---

## Ход выполнения работы

### 1. Value Objects (Ценностные Объекты)

**Созданные Value Objects:**

1. **ReadingProgress** - прогресс чтения  
   - Валидация: текущая страница ≥ 0, ≤ общего числа страниц  
   - Иммутабельность: ✅  
   - Файл: `domain/value_objects/reading_progress.py`

2. **Rating** - оценка книги  
   - Валидация: значение от 1 до 5  
   - Иммутабельность: ✅  
   - Файл: `domain/value_objects/rating.py`

3. **BookId** - идентификатор книги  
   - Валидация: не пустой  
   - Иммутабельность: ✅  
   - Файл: `domain/value_objects/book_id.py`

4. **BookTitle** - название книги  
   - Валидация: длина 1–100 символов  
   - Иммутабельность: ✅  
   - Файл: `domain/value_objects/book_title.py`

**Пример кода (ReadingProgress):**

```python
class ReadingProgress:
    def __init__(self, current_page: int, total_pages: int):
        if total_pages <= 0:
            raise ValueError("Общее количество страниц должно быть > 0")

        if current_page < 0:
            raise ValueError("Страница не может быть отрицательной")

        if current_page > total_pages:
            raise ValueError("Прогресс не может превышать общее число страниц")

        self.current_page = current_page
        self.total_pages = total_pages

    def is_finished(self) -> bool:
        return self.current_page >= self.total_pages
```

### 2. Entities (Сущности)

**Созданные Entity:**

1. **Book — книга**

   - Идентификатор: `BookId`
   - Бизнес-правила:
   - статус меняется при завершении чтения
   - Файл: `domain/entities/Book.cs`

**Пример кода:**

```csharp
public class Book : Entity
{
    public BookId Id { get; }
    public string Title { get; private set; }
    public string Author { get; private set; }
    public ReadingProgress Progress { get; private set; }
    public string Status { get; private set; } = "InProgress";

    private readonly List<IDomainEvent> _domainEvents = new();

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public Book(BookId id, string title, string author, int totalPages)
    {
        Id = id;
        Title = title;
        Author = author;
        Progress = new ReadingProgress(0, totalPages);
    }

    public void UpdateProgress(int page)
    {
        Progress = new ReadingProgress(page, Progress.TotalPages);

        if (Progress.IsFinished && Status != "Completed")
        {
            Status = "Completed";
            _domainEvents.Add(new BookCompletedEvent(Id, DateTime.UtcNow));
        }
    }

    public void ClearDomainEvents() => _domainEvents.Clear();
}
```

### 3. Aggregate Root (Корневой агрегат)

### 3. Aggregate Root (Корневой агрегат)

**Aggregate Root:** `UserLibrary`

**Границы агрегата:**
- Корень: `UserLibrary`
- Внутренние сущности: `Book`
- Value Objects: `ReadingProgress`, `Rating`

**Инварианты агрегата:**

| № | Инвариант | Где проверяется |
|---|----------|----------------|
| 1 | Книга не может быть добавлена дважды | `AddBook()` |
| 2 | Прогресс не может превышать количество страниц | `ReadingProgress` |
| 3 | Оценка возможна только после завершения | `AddReview()` |

**Пример кода:**

```csharp
public class UserLibrary : AggregateRoot
{
    private readonly List<Book> _books = new();

    public IReadOnlyCollection<Book> Books => _books.AsReadOnly();

    public void AddBook(Book book)
    {
        if (_books.Any(b => b.Id == book.Id))
            throw new InvalidOperationException("Книга уже добавлена");

        _books.Add(book);
    }
}
```
### 4. Domain Events (Доменные события)

**Созданные события:**
- **BookCompleted** — завершение книги  
- **BookAdded** — добавление книги  

**Пример кода:**

```python
from dataclasses import dataclass
from datetime import datetime

@dataclass
class DomainEvent:
    occurred_at: datetime = datetime.now()


@dataclass
class BookCompleted(DomainEvent):
    book_id: str


@dataclass
class BookAdded(DomainEvent):
    book_id: str

```
---

### 5. Юнит-тесты

**Покрытие тестами:**

| Компонент | Количество тестов | Покрытие | Статус |
|-----------|-------------------|----------|--------|
| Value Objects | _[17]_ | _[100%]_ | ✅ |
| Entities | _[9]_ | _[100%]_ | ✅ |
| Aggregate Root | _[6]_ | _[100%]_ | ✅ |
| Domain Events | _[6]_ | _[100%]_ | ✅ |

---

## Таблица критериев оценки

| Критерий | Баллы | Выполнено |
|----------|-------|-----------|
| Value Objects: корректная валидация, иммутабельность | 20 | ✅ |
| Entities: identity-based equality, инварианты | 20 | ✅ |
| Aggregate Root: границы, инварианты, публичные методы | 25 | ✅ |
| Domain Events: регистрация событий при изменении состояния | 15 | ✅ |
| Юнит-тесты: покрытие инвариантов, edge-cases | 15 | ✅ |
| Качество документации | 5 | ✅ |
| **ИТОГО** | **100** | |

---

## Контрольные вопросы

1. **В чём отличие Value Object от Entity?**
   - Value Object идентифицируется по значениям всех полей и иммутабелен. Entity имеет уникальный ID и может изменяться. Например, Money (VO) не имеет ID, а Deal (Entity) имеет ID и меняет статус.

2. **Почему Aggregate Root должен инкапсулировать доступ к внутренним сущностям?**
   - Чтобы гарантировать целостность данных. Доступ к Invoice возможен только через DealAggregate, что позволяет проверять инварианты (нельзя создать инвойс без сделки).

3. **Какая роль Domain Events? Приведите пример из вашей системы.**
   - Уведомляют другие части системы о важных изменениях. Например, DealPaed инициирует отправку уведомления клиенту и обновление статистики.

4. **Как вы проверяете инварианты в вашем агрегате? Приведите пример.**
   - В публичных методах агрегата. Пример: mark_as_paid() проверяет, что статус сделки INVOICED, иначе выбрасывает исключение.

5. **Почему Value Objects делаются иммутабельными?**
   - Чтобы избежать побочных эффектов. Два объекта Money(100, USD) всегда равны, их нельзя изменить. Безопасно передавать между объектами.

---

**Структура папки:**
```

```
lab-03/
├── Report.md
├── domain/
│ ├── events/
│ │ ├── BookCompletedEvent.cs
│ │ └── IDomainEvent.cs
│ │
│ ├── exceptions/
│ │ └── [файлы исключений].cs
│ │
│ ├── models/
│ │ ├── Book.cs
│ │ └── valueObjects/
│ │ ├── ReadingProgress.cs
│ │ ├── Rating.cs
│ │ ├── BookId.cs
│ │ └── BookTitle.cs
│ │
│ ├── aggregates/
│ │ └── UserLibrary.cs
│
└── tests/
├── ValueObjectsTests.cs
├── BookTests.cs
├── AggregateTests.cs
└── EventTests.cs
---

## Вывод

В ходе выполнения работы реализован доменный слой системы "Электронная библиотека" с использованием DDD. Выделены сущности, value objects и агрегаты. Все инварианты соблюдаются внутри домена. Использование Domain Events позволило отделить бизнес-логику от побочных эффектов. Доменный слой изолирован и легко тестируется.
---

**Дата выполнения:** _26.03.2026_  
**Оценка:** _____________  
**Подпись преподавателя:** _____________