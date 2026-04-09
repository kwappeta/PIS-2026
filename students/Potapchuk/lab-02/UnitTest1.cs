using Xunit;

public class BookServiceTests
{
    [Fact]
    public void CompleteReading_ShouldSetRatingAndStatus()
    {
        // Arrange
        var repo = new InMemoryBookRepository();
        var service = new BookService(repo);

        var book = new Book();
        var id = Guid.NewGuid();

        repo.Save(book);

        // Act
        service.Execute(id, 5);

        // Assert
        var result = repo.GetById(id);

        Assert.Equal(5, result.Rating);
        Assert.Equal("Completed", result.Status);
    }
}