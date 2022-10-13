using bookstore.Data.Dtos.Authors;
using bookstore.Data.Entities;
using bookstore.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace bookstore.Controllers
{

    [ApiController]
    [Route("publishers/{publisherId}/authors")]
    public class AuthorsController : ControllerBase
    {
        private readonly IPublishersRespository publishersRespository;
        private readonly IAuthorsRepository authorsRepository;

        public AuthorsController(IPublishersRespository publishersRespository, IAuthorsRepository authorsRepository)
        {
            this.publishersRespository = publishersRespository;
            this.authorsRepository = authorsRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<AuthorDto>> GetAll(int publisherId)
        {
            var authors = await authorsRepository.GetManyAsync(publisherId);

            return authors.Select(x => new AuthorDto(x.Id, x.FirstName, x.LastName, x.BornDate, x.Nationality));
        }

        [HttpGet("{authorId}")]
        public async Task<ActionResult<AuthorDto>> Get(int publisherId, int authorId)
        {
            var author = await authorsRepository.GetAsync(publisherId, authorId);

            if (author == null)
                return NotFound();

            return new AuthorDto(author.Id, author.FirstName, author.LastName, author.BornDate, author.Nationality);
        }

        [HttpPost]
        public async Task<ActionResult<AuthorDto>> Create(CreateAuthorDto createAuthorDto, int publisherId)
        {
            var publisher = await publishersRespository.GetAsync(publisherId);

            if (publisher == null)
                return NotFound();

            var author = new Author
            {
                FirstName = createAuthorDto.FirstName,
                LastName = createAuthorDto.LastName,
                BornDate = createAuthorDto.BornDate,
                Nationality = createAuthorDto.Nationality,
                Publisher = publisher
            };

            await authorsRepository.CreateAsync(author);

            return CreatedAtAction(nameof(Get), new { publisherId = author.Publisher.Id, authorId = author.Id }, new AuthorDto(author.Id,
                author.FirstName, author.LastName, author.BornDate, author.Nationality));
        }

        [HttpPut("{authorId}")]
        public async Task<ActionResult<AuthorDto>> Update(UpdateAuthorDto updateAuthorDto, int authorId, int publisherId)
        {
            var author = await authorsRepository.GetAsync(publisherId, authorId);

            if (author == null)
                return NotFound();

            author.FirstName = updateAuthorDto.FirstName;
            author.LastName = updateAuthorDto.LastName;

            await authorsRepository.UpdateAsync(author);

            return Ok(new AuthorDto(author.Id, author.FirstName, author.LastName, author.BornDate, author.Nationality));
        }

        [HttpDelete("{authorId}")]
        public async Task<ActionResult> Delete(int publisherId, int authorId)
        {
            var author = await authorsRepository.GetAsync(publisherId, authorId);

            if (author == null)
                return NotFound();

            await authorsRepository.DeleteAsync(author);

            return NoContent();
        }
    }
}