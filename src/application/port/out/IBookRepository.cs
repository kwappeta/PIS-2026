using BookTracker.Domain.Models;

namespace BookTracker.Application.Ports.Out
{
    public interface IBookRepository
    {
        // Найти книгу по ID
        Book GetById(Guid id);
        
        // Сохранить изменения (новую книгу или обновленный статус)
        void Save(Book book);
    }
}