using Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using MongoDB.Driver;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorDataController : ControllerBase
    {
        private readonly ISensorDataService _sensorDataService;

        public SensorDataController(ISensorDataService sensorDataService)
        {
            _sensorDataService = sensorDataService;
        }

        // Create SensorData for the specific sensor.
        [HttpPost]
        public async Task<IActionResult> CreateSensorDataAsync(SensorData sensorData)
        {
            await _sensorDataService.CreateSensorDataAsync(sensorData);

            return Ok("SensorData is stored into the database.");
        }

        // Get SensorData of the specific sensor.
        [HttpGet("sensorId")]
        public async Task<ActionResult<List<SensorData>>> GetSensorsDataBySensorIdAsync(string sensorId)
        {
            var result = await _sensorDataService.GetSensorsDataBySensorIdAsync(sensorId);

            return Ok(result);
        }

        // Get SensorData of the specific sensor with pagination.
        [HttpGet("{sensorId}/{page}/{pageSize}")]
        public async Task<ActionResult<(int totalPages, IReadOnlyList<SensorData> readOnlyList)>> GetSensorsDataPaginationAsync(string sensorId,
                                                                                                                  int page,
                                                                                                                  int pageSize)
        {
            var result = await _sensorDataService.GetSensorsDataPaginationAsync(sensorId, page, pageSize);
            
            return Ok(result);
        }

        // Delete all SensorData of the specific sensor.
        [HttpDelete("sensorId")]
        public async Task<ActionResult<DeleteResult>> DeleteAllSensorDataBySensorIdAsync(string sensorId)
        {
            var result = await _sensorDataService.DeleteAllSensorDataBySensorIdAsync(sensorId);

            return Ok(result);
        }
    }
}
