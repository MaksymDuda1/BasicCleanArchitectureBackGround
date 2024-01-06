namespace BookStore.API.Contracts;

public record BookRequest(
    string Title,
    string Description,
    decimal Price);