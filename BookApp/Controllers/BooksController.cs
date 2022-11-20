using BookApp.Data.Models;
using BookApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _service;

        public BooksController(IBookService service)
        {
            _service = service;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Book>> Get()
        {
            var items = _service.GetAll();
            return Ok(items);
        }
        [HttpGet("{id}")]
        public ActionResult<Book> Get(int id)
        {
            var item = _service.GetById(id);
            if(item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }
        [HttpPost]
        public ActionResult Save([FromBody] Book value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var item=_service.Add(value);
            return CreatedAtAction("Get",new {id=item.Id},item);
        }
        [HttpDelete("{id}")]
        public ActionResult Remove(int id)
        {
            var existingItem= _service.GetById(id);
            if(existingItem == null)
            {
                return NotFound();

            }
            _service.Remove(id);
            return Ok(existingItem);

        }
    }
}
