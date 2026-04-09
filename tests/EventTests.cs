using Xunit;

public class EventTests
{
    [Fact]
    public void Should_Create_BookCompletedEvent()
    {
        var id = Guid.NewGuid();

        var evt = new BookCompletedEvent(id, DateTime.UtcNow);

        Assert.Equal(id, evt.BookId);
    }

    [Fact]
    public void Should_Register_Event_When_Book_Completed()
    {
        var book = new Book(Guid.NewGuid(), "Test", "Author", 100);

        book.UpdateProgress(100);

        Assert.Single(book.DomainEvents);
    }

    [Fact]
    public void Should_Clear_Events()
    {
        var book = new Book(Guid.NewGuid(), "Test", "Author", 100);

        book.UpdateProgress(100);
        book.ClearEvents();

        Assert.Empty(book.DomainEvents);
    }
}