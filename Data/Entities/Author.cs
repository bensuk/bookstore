namespace bookstore.Data.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BornDate { get; set; }
        public string Nationality { get; set; }

        public Book Book { get; set; }
    }
}
