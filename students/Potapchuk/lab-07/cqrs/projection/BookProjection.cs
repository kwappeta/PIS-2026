public class BookProjection
{
    private readonly List<BookView> _storage = new();

    public void Handle(BookCreatedEvent e)
    {
        _storage.Add(new BookView
        {
            Id = e.BookId,
            Title = "Unknown",
            Progress = 0,
            IsCompleted = false
        });
    }

    public void Handle(BookCompletedEvent e)
    {
        var book = _storage.First(x => x.Id == e.BookId);
        book.IsCompleted = true;
        book.Progress = 100;
    }
}