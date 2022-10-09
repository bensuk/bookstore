using bookstore.Data.Dtos.Publishers;
using bookstore.Data.Entities;
using bookstore.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace bookstore.Controllers
{
    [ApiController]
    //[Route("api/publishers")]
    [Route("[controller]")]
    public class PublishersController : ControllerBase
    {
        private readonly IPublishersRespository publishersRespository;

        public PublishersController(IPublishersRespository publishersRespository)
        {
            this.publishersRespository = publishersRespository;
        }

        [HttpGet]
        //patikrint su actionresult ar returinna man ta 200nzn
        public async Task<IEnumerable<PublisherDto>> GetAll()
        {
            var publishers = await publishersRespository.GetManyAsync();

            return publishers.Select(x => new PublisherDto(x.Id, x.Name, x.Country, x.Founded, x.IsActive, x.NonActiveSince));
        }

        //tai irgi reiketu returnint 200
        [HttpGet]
        [Route("{publisherId}", Name = "GetPublisher")]
        public async Task<ActionResult<PublisherDto>> Get(int publisherId)
        {
            var publisher = await publishersRespository.GetAsync(publisherId);

            //404
            if (publisher == null)
                return NotFound();

            return new PublisherDto(publisher.Id, publisher.Name, publisher.Country, publisher.Founded, publisher.IsActive, publisher.NonActiveSince);
        }
        //i db ideda bet returninant erroras
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

            //201
            return CreatedAtAction("GetPublisher", new { publisherId = publisher.Id },
                new PublisherDto(publisher.Id, publisher.Name, publisher.Country, publisher.Founded, publisher.IsActive, publisher.NonActiveSince));

        }

        //padaryti kad galeciau vel grazti null ta isactive
        [HttpPut]
        [Route("{publisherId}")]
        public async Task<ActionResult<PublisherDto>> Update(int publisherId, UpdatePublisherDto updatePublisherDto)
        {
            var publisher = await publishersRespository.GetAsync(publisherId);

            //404
            if (publisher == null)
                return NotFound();

            publisher.NonActiveSince = updatePublisherDto.NonActiveSince;
            publisher.IsActive = false;

            await publishersRespository.UpdateAsync(publisher);

            return Ok(new PublisherDto(publisher.Id, publisher.Name, publisher.Country, publisher.Founded, publisher.IsActive, publisher.NonActiveSince));

        }

        [HttpDelete]
        [Route("{publisherId}")]
        public async Task<ActionResult> Delete(int publisherId)
        {
            var publisher = await publishersRespository.GetAsync(publisherId);

            if (publisher == null)
                return NotFound();

            await publishersRespository.DeleteAsync(publisher);

            //204
            return NoContent();
        }
    }
}
