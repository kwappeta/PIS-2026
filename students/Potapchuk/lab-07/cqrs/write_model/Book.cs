public class Book
{
    public Guid Id { get; }
    public string Title { get; }
    public int TotalPages { get; }
    public int CurrentPage { get; private set; }

    public List<object> DomainEvents = new();

    public Book(Guid id, string title, int totalPages)
    {
        Id = id;
        Title = title;
        TotalPages = totalPages;
        CurrentPage = 0;

        DomainEvents.Add(new BookCreatedEvent(id));
    }

    public void UpdateProgress(int page)
    {
        if (page > TotalPages)
            throw new Exception("Превышение страниц");

        CurrentPage = page;

        if (CurrentPage == TotalPages)
            DomainEvents.Add(new BookCompletedEvent(Id));
    }
}