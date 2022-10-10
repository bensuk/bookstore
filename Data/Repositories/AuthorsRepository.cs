using bookstore.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace bookstore.Data.Repositories
{
    public interface IAuthorsRepository
    {
        Task CreateAsync(Author author);
        Task DeleteAsync(Author author);
        Task<Author?> GetAsync(int authorId);
        Task<IReadOnlyList<Author>> GetManyAsync();
        Task UpdateAsync(Author author);
    }

    public class AuthorsRepository : IAuthorsRepository
    {
        private readonly BookstoreDbContex bookstoreDbContex;
        public AuthorsRepository(BookstoreDbContex bookstoreDbContex)
        {
            this.bookstoreDbContex = bookstoreDbContex;
        }

        public async Task<Author?> GetAsync(int authorId)
        {
            return await bookstoreDbContex.Authors.FirstOrDefaultAsync(x => x.Id == authorId);
        }

        public async Task<IReadOnlyList<Author>> GetManyAsync()
        {
            return await bookstoreDbContex.Authors.ToListAsync();
        }

        public async Task CreateAsync(Author author)
        {
            bookstoreDbContex.Authors.Add(author);
            await bookstoreDbContex.SaveChangesAsync();
        }

        public async Task UpdateAsync(Author author)
        {
            bookstoreDbContex.Authors.Update(author);
            await bookstoreDbContex.SaveChangesAsync();
        }
        public async Task DeleteAsync(Author author)
        {
            bookstoreDbContex.Authors.Remove(author);
            await bookstoreDbContex.SaveChangesAsync();
        }
    }
}
