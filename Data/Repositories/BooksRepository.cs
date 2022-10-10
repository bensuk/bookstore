using bookstore.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace bookstore.Data.Repositories
{
    public interface IBooksRepository
    {
        Task CreateAsync(Book book);
        Task DeleteAsync(Book book);
        Task<Book?> GetAsync(int bookId);
        Task<IReadOnlyList<Book>> GetManyAsync();
        Task UpdateAsync(Book book);
    }

    public class BooksRepository : IBooksRepository
    {
        private readonly BookstoreDbContex bookstoreDbContex;

        public BooksRepository(BookstoreDbContex bookstoreDbContex)
        {
            this.bookstoreDbContex = bookstoreDbContex;
        }

        public async Task<Book?> GetAsync(int bookId)
        {
            return await bookstoreDbContex.Books.FirstOrDefaultAsync(x => x.Id == bookId);
        }

        public async Task<IReadOnlyList<Book>> GetManyAsync()
        {
            return await bookstoreDbContex.Books.ToListAsync();
        }

        public async Task CreateAsync(Book book)
        {
            bookstoreDbContex.Books.Add(book);
            await bookstoreDbContex.SaveChangesAsync();
        }

        public async Task UpdateAsync(Book book)
        {
            bookstoreDbContex.Books.Update(book);
            await bookstoreDbContex.SaveChangesAsync();
        }

        public async Task DeleteAsync(Book book)
        {
            bookstoreDbContex.Books.Remove(book);
            await bookstoreDbContex.SaveChangesAsync();
        }
    }
}
