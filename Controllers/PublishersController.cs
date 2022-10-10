using bookstore.Data.Dtos.Publishers;
using bookstore.Data.Entities;
using bookstore.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace bookstore.Controllers
{
    [ApiController]
    [Route("publishers")]
    public class PublishersController : ControllerBase
    {
        private readonly IPublishersRespository publishersRespository;

        public PublishersController(IPublishersRespository publishersRespository)
        {
            this.publishersRespository = publishersRespository;
        }

        [HttpGet]
        public async Task<IEnumerable<PublisherDto>> GetAll()
        {
            var publishers = await publishersRespository.GetManyAsync();

            return publishers.Select(x => new PublisherDto(x.Id, x.Name, x.Country, x.Founded, x.IsActive, x.NonActiveSince));
        }

        [HttpGet("{publisherId}", Name = "GetPublisher")]
        public async Task<ActionResult<PublisherDto>> Get(int publisherId)
        {
            var publisher = await publishersRespository.GetAsync(publisherId);

            if (publisher == null)
                return NotFound();

            return new PublisherDto(publisher.Id, publisher.Name, publisher.Country, publisher.Founded,
                publisher.IsActive, publisher.NonActiveSince);
        }

        [HttpPost]
        public async Task<ActionResult<PublisherDto>> Create(CreatePublisherDto createPublisherDto)
        {
            var publisher = new Publisher
            {
                Name = createPublisherDto.Name,
                Country = createPublisherDto.Country,
                Founded = createPublisherDto.Founded,
                IsActive = createPublisherDto.NonActiveSince is null ? true : false,
                NonActiveSince = createPublisherDto.NonActiveSince
            };

            await publishersRespository.CreateAsync(publisher);

            return CreatedAtAction(nameof(Get), new { publisherId = publisher.Id }, new PublisherDto(publisher.Id,
                publisher.Name, publisher.Country, publisher.Founded, publisher.IsActive, publisher.NonActiveSince));
        }

        [HttpPut]
        [Route("{publisherId}")]
        public async Task<ActionResult<PublisherDto>> Update(int publisherId, UpdatePublisherDto updatePublisherDto)
        {
            var publisher = await publishersRespository.GetAsync(publisherId);

            if (publisher == null)
                return NotFound();

            publisher.NonActiveSince = updatePublisherDto.NonActiveSince;
            publisher.IsActive = publisher.NonActiveSince is null ? true : false;

            await publishersRespository.UpdateAsync(publisher);

            return Ok(new PublisherDto(publisher.Id, publisher.Name, publisher.Country, publisher.Founded,
                publisher.IsActive, publisher.NonActiveSince));
        }

        [HttpDelete]
        [Route("{publisherId}")]
        public async Task<ActionResult> Delete(int publisherId)
        {
            var publisher = await publishersRespository.GetAsync(publisherId);

            if (publisher == null)
                return NotFound();

            await publishersRespository.DeleteAsync(publisher);

            return NoContent();
        }
    }
}
