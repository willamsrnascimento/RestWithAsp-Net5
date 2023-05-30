using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestWithAspNet.Business;
using System.Threading.Tasks;
using RestWithAspNet.Data.VO;
using RestWithAspNet.Hypermedia.Filters;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace RestWithAspNet.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Authorize("Bearer")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IPersonBusiness _personBusiness;

        public PersonController(ILogger<PersonController> logger, IPersonBusiness personBusiness)
        {
            _logger = logger;
            _personBusiness = personBusiness;
        }

        [HttpGet("{sortDirection}/{pageSize}/{page}")]
        [ProducesResponseType(200, Type = typeof(List<PersonVO>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HypermediaFilter))]
        public async Task<IActionResult> Get([FromQuery] string name, string sortDirection, int pageSize, int page)
        {
            return Ok(await _personBusiness.FindWithPagedSearchAsync(name, sortDirection, pageSize, page));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(PersonVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HypermediaFilter))]
        public async Task<IActionResult> Get(long id)
        {
            PersonVO personVO = await _personBusiness.FindByIdAsync(id);

            if(personVO == null)
            {
                return NotFound();
            }

            return Ok(personVO);
        } 
        
        [HttpGet("findPersonByName")]
        [ProducesResponseType(200, Type = typeof(PersonVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HypermediaFilter))]
        public async Task<IActionResult> Get([FromQuery] string firstName, [FromQuery] string lastName)
        {
            var personVO = await _personBusiness.FindByNameAsync(firstName, lastName);

            if(personVO == null)
            {
                return NotFound();
            }

            return Ok(personVO);
        }


        [HttpPost]
        [ProducesResponseType(200, Type = typeof(PersonVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HypermediaFilter))]
        public async Task<IActionResult> Post([FromBody] PersonVO personVO)
        {
            if(personVO == null)
            {
                return BadRequest();
            }

            return Ok(await _personBusiness.CreateAsync(personVO));
        }

        [HttpPut]
        [ProducesResponseType(200, Type = typeof(PersonVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HypermediaFilter))]
        public async Task<IActionResult> Put([FromBody] PersonVO personVO)
        {
            if (personVO == null)
            {
                return BadRequest();
            }

            return Ok(await _personBusiness.UpdateAsync(personVO));
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(200, Type = typeof(PersonVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HypermediaFilter))]
        public async Task<IActionResult> Patch(long id)
        {
            PersonVO personVO = await _personBusiness.DisableAsync(id);

            return Ok(personVO);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Delete(long id)
        {
            await _personBusiness.DeleteAsync(id);
            return NoContent();
        }



    }
}
