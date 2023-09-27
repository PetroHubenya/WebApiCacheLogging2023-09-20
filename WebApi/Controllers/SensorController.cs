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
            await _sensorService.CreateSensorAsync(sensor);

            return Ok("Sensor is created.");
        }

        // Get all sensors.
        [HttpGet]
        public async Task<ActionResult<List<Sensor>>> GetAllSensorsAsync()
        {
            var result = await _sensorService.GetAllSensorsAsync();

            return Ok(result);
        }

        // Get sensor by id.
        [HttpGet("id")]
        public async Task<ActionResult<Sensor>> GetSensorAsync(string id)
        {
            var result = await _sensorService.GetSensorAsync(id);

            return Ok(result);
        }

        // Update sensor.
        [HttpPut("id")]
        public async Task<IActionResult> UpdateSensorAsync(string id, Sensor newSensor)
        {
            await _sensorService.UpdateSensorAsync(id, newSensor);

            return Ok("Sensor updated successfully.");
        }

        // Delete sensor.
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteSensorAsync(string id)
        {
            await _sensorService.DeleteSensorAsync(id);

            return Ok("Sensor deleted successfully.");
        }
    }
}
