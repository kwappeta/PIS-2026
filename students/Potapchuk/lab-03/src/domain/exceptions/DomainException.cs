namespace Domain.Exceptions;

// Базовый класс для всех бизнес-ошибок твоего проекта
public class DomainException : Exception
{
    public DomainException(string message) : base(message) { }
}