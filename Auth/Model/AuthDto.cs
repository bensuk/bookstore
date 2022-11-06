using System.ComponentModel.DataAnnotations;

namespace bookstore.Auth.Model
{
    public record RegisterUserDto([Required] string UserName, [EmailAddress][Required] string Email, [Required] string Password);
    public record LoginUserDto(string UserName, string Password);
}
