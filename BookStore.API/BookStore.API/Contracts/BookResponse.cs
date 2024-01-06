namespace BookStore.API.Contracts;

public record BookResponse(
    Guid Id,
    string Title,
    string Description,
    decimal Price);