using bookstore.Auth.Model;
using System.ComponentModel.DataAnnotations;

namespace bookstore.Data.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAccepted { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime? LastUpdated { get; set; }

        public Author Author { get; set; }

        //[Required]
        public string? UserId { get; set; }
        public BookstoreUser User { get; set; }
    }
}
