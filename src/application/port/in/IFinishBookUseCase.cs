namespace BookTracker.Application.Ports.In
{
    public interface IFinishBookUseCase
    {
        // Команда на завершение чтения книги
        void Execute(Guid bookId, int rating);
    }
}