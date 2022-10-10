namespace bookstore.Data.Dtos.Authors
{
    public record AuthorDto(int Id, string FirstName, string LastName, DateTime BornDate, string Nationality);
    public record CreateAuthorDto(string FirstName, string LastName, DateTime BornDate, string Nationality);
    public record UpdateAuthorDto(string FirstName, string LastName);
}
