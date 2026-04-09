using Xunit;

public class ValueObjectsTests
{
    [Fact]
    public void Should_Create_Valid_ReadingProgress()
    {
        var progress = new ReadingProgress(10, 100);

        Assert.Equal(10, progress.CurrentPage);
        Assert.Equal(100, progress.TotalPages);
    }

    [Fact]
    public void Should_Throw_When_Page_Negative()
    {
        Assert.Throws<ArgumentException>(() =>
            new ReadingProgress(-1, 100));
    }

    [Fact]
    public void Should_Throw_When_Page_Exceeds_Total()
    {
        Assert.Throws<ArgumentException>(() =>
            new ReadingProgress(101, 100));
    }

    [Fact]
    public void Should_Throw_When_Total_Invalid()
    {
        Assert.Throws<ArgumentException>(() =>
            new ReadingProgress(0, 0));
    }

    [Fact]
    public void Should_Be_Finished_When_Last_Page()
    {
        var progress = new ReadingProgress(100, 100);

        Assert.True(progress.IsFinished);
    }
}