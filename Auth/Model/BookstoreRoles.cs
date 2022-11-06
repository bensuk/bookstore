namespace bookstore.Auth.Model
{
    public static class BookstoreRoles
    {
        public const string Admin = nameof(Admin);
        public const string User = nameof(User);

        public static readonly IReadOnlyCollection<string> All = new[] { Admin, User };
    }
}
