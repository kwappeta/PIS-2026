using BookTracker.Application.Ports.In;
using BookTracker.Application.Ports.Out;
using BookTracker.Domain.Models;

namespace BookTracker.Application.Services
{
    public class BookService : IFinishBookUseCase
    {
        private readonly IBookRepository _repository;

        // ВАЖНО: Мы зависим от ИНТЕРФЕЙСА, а не от конкретной БД
        public BookService(IBookRepository repository)
        {
            _repository = repository;
        }

        public void Execute(Guid bookId, int rating)
        {
            // 1. Получаем книгу через порт
            var book = _repository.GetById(bookId);
            
            if (book != null)
            {
                // 2. Вызываем бизнес-логику из Домена
                book.CompleteReading(rating);
                
                // 3. Сохраняем результат через порт
                _repository.Save(book);
            }
        }
    }
}