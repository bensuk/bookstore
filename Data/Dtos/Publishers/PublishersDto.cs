using System.Security.Cryptography.X509Certificates;

namespace bookstore.Data.Dtos.Publishers
{
    public record PublisherDto(int Id, string Name, string Country, int Founded, bool IsActive, int? NonActiveSince);
    public record CreatePublisherDto(string Name, string Country, int Founded, int? NonActiveSince);
    public record UpdatePublisherDto(int NonActiveSince);
}
