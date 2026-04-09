using Xunit;

public class AggregateTests
{
    [Fact]
    public void Should_Add_Book()
    {
        var library = new UserLibrary();
        var book = new Book(Guid.NewGuid(), "Test", "Author", 100);

        library.AddBook(book);

        Assert.Single(library.Books);
    }

    [Fact]
    public void Should_Not_Add_Duplicate_Book()
    {
        var library = new UserLibrary();
        var book = new Book(Guid.NewGuid(), "Test", "Author", 100);

        library.AddBook(book);

        Assert.Throws<InvalidOperationException>(() =>
            library.AddBook(book));
    }

    [Fact]
    public void Should_Contain_Added_Book()
    {
        var library = new UserLibrary();
        var book = new Book(Guid.NewGuid(), "Test", "Author", 100);

        library.AddBook(book);

        Assert.Contains(book, library.Books);
    }
}