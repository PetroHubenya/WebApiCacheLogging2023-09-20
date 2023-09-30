using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorController : ControllerBase
    {
        private readonly ISensorService _sensorService;

        public SensorController(ISensorService sensorService)
        {
            _sensorService = sensorService;
        }

        // Create sensor.
        [HttpPost]
        public async Task<IActionResult> CreateSensorAsync(Sensor sensor)
        {
            try
            {
                if (sensor == null)
                {
                    throw new ArgumentNullException(nameof(sensor));
                }
                else
                {
                    await _sensorService.CreateSensorAsync(sensor);

                    return Ok("The sensor is created.");
                }
            }
            catch
            {
                throw;
            }
        }

        // Get all sensors.
        [HttpGet]
        public async Task<ActionResult<List<Sensor>>> GetAllSensorsAsync()
        {
            try
            {
                var result = await _sensorService.GetAllSensorsAsync();

                if (result == null)
                {
                    return NotFound("The list of sensors is not found");
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

        // Get sensor by id.
        [HttpGet("id")]
        public async Task<ActionResult<Sensor>> GetSensorAsync(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    throw new ArgumentNullException(nameof(id));
                }
                else
                {
                    var result = await _sensorService.GetSensorAsync(id);

                    if (result == null)
                    {
                        return NotFound($"The sensor with the {id} Id is not found.");
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

        // Update sensor.
        [HttpPut("id")]
        public async Task<IActionResult> UpdateSensorAsync(string id, Sensor newSensor)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    throw new ArgumentNullException(nameof(id));
                }
                else if (newSensor == null)
                {
                    throw new ArgumentNullException(nameof(newSensor));
                }
                else
                {
                    await _sensorService.UpdateSensorAsync(id, newSensor);

                    return Ok($"The sensor with the {id} Id is updated successfully.");
                }
            }
            catch
            {
                throw;
            }
        }

        // Delete sensor.
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteSensorAsync(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    throw new ArgumentNullException(nameof(id));
                }
                else
                {
                    await _sensorService.DeleteSensorAsync(id);

                    return Ok($"The sensor with the {id} Id is deleted successfully.");
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
