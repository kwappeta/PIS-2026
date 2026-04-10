using Xunit;
using System.Linq;

public class ProjectionTests
{
    [Fact]
    public void Handle_BookCreatedEvent_ShouldUpdateStorage()
    {
        // Arrange
        var projection = new BookProjection();
        var bookId = Guid.NewGuid();
        var createEvent = new BookCreatedEvent(bookId);

        // Act
        projection.Handle(createEvent);

        // Assert
        var result = projection.GetById(bookId);
        Assert.NotNull(result);
        Assert.Equal(bookId, result.Id);
        Assert.False(result.IsCompleted);
    }

    [Fact]
    public void Handle_BookCompletedEvent_ShouldSetCompletedStatus()
    {
        // Arrange
        var projection = new BookProjection();
        var bookId = Guid.NewGuid();
        projection.Handle(new BookCreatedEvent(bookId));

        // Act
        projection.Handle(new BookCompletedEvent(bookId));

        // Assert
        var result = projection.GetById(bookId);
        Assert.True(result.IsCompleted);
        Assert.Equal(100, result.Progress);
    }
}