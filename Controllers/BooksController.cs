using bookstore.Data.Dtos.Books;
using bookstore.Data.Entities;
using bookstore.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace bookstore.Controllers
{
    [ApiController]
    [Route("publishers/{publisherId}/authors/{authorId}/books")]
    public class BooksController : ControllerBase
    {
        private readonly IPublishersRespository publishersRespository;
        private readonly IAuthorsRepository authorsRepository;
        private readonly IBooksRepository booksRepository;

        public BooksController(IPublishersRespository publishersRespository, IAuthorsRepository authorsRepository, IBooksRepository booksRepository)
        {
            this.publishersRespository = publishersRespository;
            this.authorsRepository = authorsRepository;
            this.booksRepository = booksRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<BookDto>> GetAll(int publisherId, int authorId)
        {
            var books = await booksRepository.GetManyAsync(publisherId, authorId);

            return books.Select(x => new BookDto(x.Id, x.Name, x.ReleaseDate, x.Description));
        }

        [HttpGet("{bookId}")]
        public async Task<ActionResult<BookDto>> Get(int publisherId, int authorId, int bookId)
        {
            var book = await booksRepository.GetAsync(publisherId, authorId, bookId);

            if (book == null)
                return NotFound();

            return new BookDto(book.Id, book.Name, book.ReleaseDate, book.Description);
        }

        [HttpPost]
        public async Task<ActionResult<BookDto>> Create(int publisherId, int authorId, CreateBookDto createBookDto)
        {
            var publisher = await publishersRespository.GetAsync(publisherId);
            var author = await authorsRepository.GetAsync(publisherId, authorId);

            if (publisher == null || author == null)
                return NotFound();

            var book = new Book
            {
                Name = createBookDto.Name,
                IsAccepted = false,
                ReleaseDate = createBookDto.ReleaseDate,
                Description = createBookDto.Description,
                AddedDate = DateTime.UtcNow,
                Author = author
            };

            await booksRepository.CreateAsync(book);

            return CreatedAtAction(nameof(Get), new { publisherId = book.Author.Publisher.Id, authorId = book.Author.Id,
                bookId = book.Id }, new BookDto(book.Id, book.Name, book.ReleaseDate, book.Description));
        }

        [HttpPut("{bookId}")]
        public async Task<ActionResult<BookDto>> Update(int publisherId, int authorId, int bookId, UpdateBookDto updateBookDto)
        {
            var book = await booksRepository.GetAsync(publisherId, authorId, bookId);

            if (book == null)
                return NotFound();

            book.Description = updateBookDto.Description;

            await booksRepository.UpdateAsync(book);

            return Ok(new BookDto(book.Id, book.Name, book.ReleaseDate, book.Description));
        }

        [HttpDelete("{bookId}")]
        public async Task<ActionResult> Delete(int publisherId, int authorId, int bookId)
        {
            var book = await booksRepository.GetAsync(publisherId, authorId, bookId);

            if (book == null)
                return NotFound();

            await booksRepository.DeleteAsync(book);

            return NoContent();
        }
    }
}
