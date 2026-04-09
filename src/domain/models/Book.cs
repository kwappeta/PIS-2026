using Domain.Events;
using Domain.Models.ValueObjects;

namespace Domain.Models;

public class Book
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public ReadingProgress Progress { get; private set; }
    public string Status { get; private set; }

    // Список событий, которые произошли с книгой
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public Book(string title, int totalPages)
    {
        Id = Guid.NewGuid();
        Title = title;
        Progress = new ReadingProgress(0, totalPages);
        Status = "New";
    }

    public void UpdateProgress(int page)
    {
        this.Progress = new ReadingProgress(page, this.Progress.TotalPages);

        if (this.Progress.IsFinished && Status != "Completed")
        {
            this.Status = "Completed";
            // Регистрируем событие!
            _domainEvents.Add(new BookCompletedEvent(this.Id, DateTime.UtcNow));
        }
    }

    public void ClearEvents() => _domainEvents.Clear();
}