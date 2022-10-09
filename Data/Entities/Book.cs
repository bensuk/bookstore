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

        public Publisher Publisher { get; set; }
    }
}
