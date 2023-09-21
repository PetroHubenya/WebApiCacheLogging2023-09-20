using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoxController : ControllerBase
    {
        private readonly IBoxService _boxService;

        public BoxController(IBoxService boxService)
        {
            _boxService = boxService;
        }

        // GET: api/<BoxController>
        [HttpGet]
        public async Task<ActionResult<List<Box>>> GetAllBoxesAsync()
        {
            var result = await _boxService.GetAllBoxesAsync();
            return Ok(result);
        }

        // GET api/<BoxController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BoxController>
        [HttpPost]
        public async Task<ActionResult<Box>> PostBoxAsync([FromBody] Box box)
        {
            await _boxService.CreateBoxAsync(box);
            return Ok(box);
        }

        // PUT api/<BoxController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BoxController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
