namespace bookstore.Data.Dtos.Books
{
    public record BookDto(int Id, string Name, DateTime ReleaseDate, string Description);
    public record CreateBookDto(string Name, DateTime ReleaseDate, string Description);
    public record UpdateBookDto(string Description);
}