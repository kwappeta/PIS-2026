using BookTracker.Domain.Models;
using BookTracker.Application.Ports.Out;
using System.Collections.Generic;
using System.Linq;

namespace BookTracker.Infrastructure.Adapters.Out
{
    public class InMemoryBookRepository : IBookRepository
    {
        // Наше временное хранилище (вместо БД)
        private readonly List<Book> _books = new();

        public Book GetById(Guid id)
        {
            return _books.FirstOrDefault(b => b.Id == id);
        }

        public void Save(Book book)
        {
            // Если книга уже есть, удаляем старую версию и добавляем новую
            var existing = GetById(book.Id);
            if (existing != null) _books.Remove(existing);
            
            _books.Add(book);
        }
    }
}