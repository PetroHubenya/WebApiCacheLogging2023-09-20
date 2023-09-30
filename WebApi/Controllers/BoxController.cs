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
            try
            {
                var result = await _boxService.GetAllBoxesAsync();

                if (result == null)
                {
                    return NotFound("Couldn't get all boxes.");
                }
                else
                {
                    return Ok(result);
                }
            }
            catch
            {
                throw;
            }
        }

        // GET api/<BoxController>/5            // Get box by id.
        [HttpGet("{id}")]
        public async Task<ActionResult<Box>> GetBoxAsync(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    throw new ArgumentNullException(nameof(id));
                }
                else
                {
                    var result = await _boxService.GetBoxAsync(id);

                    if (result == null)
                    {
                        return NotFound($"The box with the {id} Id is not found.");
                    }
                    else
                    {
                        return Ok(result);
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        // POST api/<BoxController>             // Create box.
        [HttpPost]
        public async Task<IActionResult> PostBoxAsync([FromBody] Box box)
        {
            try
            {
                if (box == null)
                {
                    throw new ArgumentNullException(nameof (box));
                }
                else
                {
                    await _boxService.CreateBoxAsync(box);

                    return Ok("The box is created.");
                }
            }
            catch
            {
                throw;
            }
        }

        // PUT api/<BoxController>/5            // Update box.
        [HttpPut("id")]
        public async Task<IActionResult> UpdateBoxAsync(string id, [FromBody] Box newBox)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    throw new ArgumentNullException(nameof (id));
                }
                else if (newBox == null)
                {
                    throw new ArgumentNullException(nameof (newBox));
                }
                else
                {
                    await _boxService.UpdateBoxAsync(id, newBox);

                    return Ok($"The box with the {id} Id is updated.");
                }
            }
            catch
            {
                throw;
            }
        }

        // DELETE api/<BoxController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoxAsync(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    throw new ArgumentNullException(nameof (id));
                }
                else
                {
                    await _boxService.DeleteBoxAsync(id);

                    return Ok($"The box with the {id} Id is deleted.");
                }
            }
            catch
            {
                throw;
            }
        }

        // Add sensor to the box.
        [HttpPost("{boxId}/{sensorId}")]
        public async Task<IActionResult> AddSensorToBoxAsync(string boxId, string sensorId)
        {
            try
            {
                if (string.IsNullOrEmpty(boxId))
                {
                    throw new ArgumentNullException(nameof (boxId));
                }
                else if (string.IsNullOrEmpty(sensorId))
                {
                    throw new ArgumentNullException(nameof (sensorId));
                }
                else
                {
                    await _boxService.AddSensorToBoxAsync(boxId, sensorId);

                    return Ok($"The sensor with the {sensorId} Id is added to the box with the {boxId} Id.");
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
