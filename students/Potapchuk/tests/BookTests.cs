using Xunit;

public class BookTests
{
    [Fact]
    public void Should_Create_Book()
    {
        var book = new Book(Guid.NewGuid(), "Test", "Author", 100);

        Assert.Equal("Test", book.Title);
        Assert.False(book.IsCompleted);
    }

    [Fact]
    public void Should_Update_Progress()
    {
        var book = new Book(Guid.NewGuid(), "Test", "Author", 100);

        book.UpdateProgress(50);

        Assert.Equal(50, book.Progress.CurrentPage);
    }

    [Fact]
    public void Should_Complete_Book()
    {
        var book = new Book(Guid.NewGuid(), "Test", "Author", 100);

        book.UpdateProgress(100);

        Assert.True(book.IsCompleted);
    }

    [Fact]
    public void Should_Add_Event_On_Complete()
    {
        var book = new Book(Guid.NewGuid(), "Test", "Author", 100);

        book.UpdateProgress(100);

        Assert.Single(book.DomainEvents);
    }

    [Fact]
    public void Should_Not_Update_When_Completed()
    {
        var book = new Book(Guid.NewGuid(), "Test", "Author", 100);

        book.UpdateProgress(100);

        Assert.Throws<InvalidOperationException>(() =>
            book.UpdateProgress(50));
    }
}