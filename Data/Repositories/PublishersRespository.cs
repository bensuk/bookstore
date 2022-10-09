using bookstore.Data.Dtos.Publishers;
using bookstore.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace bookstore.Data.Repositories
{
    public interface IPublishersRespository
    {
        Task CreateAsync(Publisher publisher);
        Task DeleteAsync(Publisher publisher);
        Task<Publisher?> GetAsync(int publisherId);
        Task<IReadOnlyList<Publisher>> GetManyAsync();
        Task UpdateAsync(Publisher publisher);
    }

    public class PublishersRespository : IPublishersRespository
    {
        private readonly BookstoreDbContex bookstoreDbContex;

        public PublishersRespository(BookstoreDbContex bookstoreDbContex)
        {
            this.bookstoreDbContex = bookstoreDbContex;
        }

        public async Task<Publisher?> GetAsync(int publisherId)
        {
            return await bookstoreDbContex.Publishers.FirstOrDefaultAsync(x => x.Id == publisherId);
        }

        //sita padaryti su pagenation
        public async Task<IReadOnlyList<Publisher>> GetManyAsync()
        {
            return await bookstoreDbContex.Publishers.ToListAsync();
        }

        public async Task CreateAsync(Publisher publisher)
        {
            bookstoreDbContex.Publishers.Add(publisher);
            await bookstoreDbContex.SaveChangesAsync();
        }

        public async Task UpdateAsync(Publisher publisher)
        {
            bookstoreDbContex.Publishers.Update(publisher);
            await bookstoreDbContex.SaveChangesAsync();
        }

        public async Task DeleteAsync(Publisher publisher)
        {
            bookstoreDbContex.Publishers.Remove(publisher);
            await bookstoreDbContex.SaveChangesAsync();
        }
    }
}
