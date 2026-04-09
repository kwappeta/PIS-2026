using System.Text.RegularExpressions;
using Domain.Exceptions;

namespace Domain.Models.ValueObjects;

public record Isbn
{
    public string Value { get; init; }

    public Isbn(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("ISBN не может быть пустым.");

        var cleanValue = value.Replace("-", "").Replace(" ", "");

        if (!Regex.IsMatch(cleanValue, @"^(\d{10}|\d{13})$"))
            throw new DomainException("ISBN должен содержать 10 или 13 цифр.");

        Value = cleanValue;
    }

    public override string ToString() => Value;
}