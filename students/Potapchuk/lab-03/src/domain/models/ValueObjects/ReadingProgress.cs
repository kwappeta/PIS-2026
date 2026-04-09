using Domain.Exceptions;

namespace Domain.Models.ValueObjects;

public record ReadingProgress
{
    // Свойства только для чтения (Immutable)
    public int CurrentPage { get; init; }
    public int TotalPages { get; init; }

    // Конструктор с валидацией инвариантов
    public ReadingProgress(int currentPage, int totalPages)
    {
        if (totalPages <= 0)
            throw new DomainException("В книге должна быть хотя бы одна страница.");

        if (currentPage < 0)
            throw new DomainException("Текущая страница не может быть отрицательной.");

        if (currentPage > totalPages)
            throw new DomainException($"Прогресс ({currentPage}) не может превышать объем книги ({totalPages}).");

        CurrentPage = currentPage;
        TotalPages = totalPages;
    }

    // Полезные методы (бизнес-логика внутри VO)
    public bool IsFinished => CurrentPage == TotalPages;
    
    public double GetPercentage() => 
        TotalPages == 0 ? 0 : Math.Round((double)CurrentPage / TotalPages * 100, 2);

    public override string ToString() => $"{CurrentPage}/{TotalPages} ({GetPercentage()}%)";
}