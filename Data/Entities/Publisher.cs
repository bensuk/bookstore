namespace bookstore.Data.Entities
{
    public class Publisher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public int Founded  { get; set; }
        public bool IsActive { get; set; }
        public int? NonActiveSince { get; set; }
    }
}
