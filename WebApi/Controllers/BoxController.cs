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

        // GET: api/<BoxController>             // Get all boxes
        [HttpGet]
        public async Task<ActionResult<List<Box>>> GetAllBoxesAsync()
        {
            var result = await _boxService.GetAllBoxesAsync();

            return Ok(result);
        }

        // GET api/<BoxController>/5            // Get box by id.
        [HttpGet("{id}")]
        public async Task<ActionResult<Box>> GetBoxAsync(string id)
        {
            var result = await _boxService.GetBoxAsync(id);

            return Ok(result);
        }

        // POST api/<BoxController>             // Create box.
        [HttpPost]
        public async Task<IActionResult> PostBoxAsync([FromBody] Box box)
        {
            await _boxService.CreateBoxAsync(box);

            return Ok("The box is created.");
        }

        // PUT api/<BoxController>/5            // Update box.
        [HttpPut("id")]
        public async Task<IActionResult> UpdateBoxAsync(string id, [FromBody] Box newBox)
        {
            await _boxService.UpdateBoxAsync(id, newBox);

            return Ok("The box is updated.");
        }

        // DELETE api/<BoxController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoxAsync(string id)
        {
            await _boxService.DeleteBoxAsync(id);

            return Ok("The box is deleted.");
        }

        // Add sensor to the box.
        [HttpPost("{boxId}/{sensorId}")]
        public async Task<IActionResult> AddSensorToBoxAsync(string boxId, string sensorId)
        {
            await _boxService.AddSensorToBoxAsync(boxId, sensorId);

            return Ok("The sensor is added to the box.");
        }
    }
}
