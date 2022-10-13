using bookstore.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace bookstore.Data.Repositories
{
    public interface IBooksRepository
    {
        Task CreateAsync(Book book);
        Task DeleteAsync(Book book);
        Task<Book?> GetAsync(int publisherId, int authorId, int bookId);
        Task<IReadOnlyList<Book>> GetManyAsync(int publisherId, int authorId);
        Task UpdateAsync(Book book);
    }

    public class BooksRepository : IBooksRepository
    {
        private readonly BookstoreDbContex bookstoreDbContex;

        public BooksRepository(BookstoreDbContex bookstoreDbContex)
        {
            this.bookstoreDbContex = bookstoreDbContex;
        }

        public async Task<Book?> GetAsync(int publisherId, int authorId, int bookId)
        {
            return await bookstoreDbContex.Books.FirstOrDefaultAsync(x => x.Author.Publisher.Id == publisherId && x.Author.Id == authorId && x.Id == bookId);
        }

        public async Task<IReadOnlyList<Book>> GetManyAsync(int publisherId, int authorId)
        {
            return await bookstoreDbContex.Books.Where(x => x.Author.Publisher.Id == publisherId && x.Author.Id == authorId).ToListAsync();
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
