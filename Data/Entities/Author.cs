using bookstore.Auth.Model;
using System.ComponentModel.DataAnnotations;

namespace bookstore.Data.Entities
{
    public class Author : IUserOwnedResource
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BornDate { get; set; }
        public string Nationality { get; set; }

        public Publisher Publisher { get; set; }

        //[Required]
        public string? UserId { get; set; }
        public BookstoreUser User { get; set; }
    }
}
