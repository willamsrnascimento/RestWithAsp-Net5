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
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly IBookBusiness _bookBusiness;

        public BookController(ILogger<BookController> logger, IBookBusiness bookBusiness)
        {
            _logger = logger;
            _bookBusiness = bookBusiness;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<BookVO>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HypermediaFilter))]
        public async Task<IActionResult> Get()
        {
            return Ok(await _bookBusiness.FindAllAsync());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(BookVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HypermediaFilter))]
        public async Task<IActionResult> Get(long id)
        {
            BookVO bookVO = await _bookBusiness.FindByIdAsync(id);

            if(bookVO == null)
            {
                return NotFound();
            }

            return Ok(bookVO);
        }


        [HttpPost]
        [ProducesResponseType(200, Type = typeof(BookVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HypermediaFilter))]
        public async Task<IActionResult> Post([FromBody] BookVO bookVO)
        {
            if(bookVO == null)
            {
                return BadRequest();
            }

            return Ok(await _bookBusiness.CreateAsync(bookVO));
        }

        [HttpPut]
        [ProducesResponseType(200, Type = typeof(BookVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HypermediaFilter))]
        public async Task<IActionResult> Put([FromBody] BookVO bookVO)
        {
            if (bookVO == null)
            {
                return BadRequest();
            }

            return Ok(await _bookBusiness.UpdateAsync(bookVO));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Delete(long id)
        {
            await _bookBusiness.DeleteAsync(id);
            return NoContent();
        }



    }
}
