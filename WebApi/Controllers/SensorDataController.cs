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
            try
            {
                if (sensorData == null)
                {
                    throw new ArgumentNullException(nameof(sensorData));
                }
                else
                {
                    await _sensorDataService.CreateSensorDataAsync(sensorData);

                    return Ok("SensorData is stored into the database.");
                }
            }
            catch
            {
                throw;
            }
        }

        // Get SensorData of the specific sensor.
        [HttpGet("sensorId")]
        public async Task<ActionResult<List<SensorData>>> GetSensorsDataBySensorIdAsync(string sensorId)
        {
            try
            {
                if (string.IsNullOrEmpty(sensorId))
                {
                    throw new ArgumentNullException(nameof(sensorId));
                }
                else
                {
                    var result = await _sensorDataService.GetSensorsDataBySensorIdAsync(sensorId);

                    if (result == null)
                    {
                        return NotFound($"The list of SensorData for the sensor with the {sensorId} Id was not found.");
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

        // Get SensorData of the specific sensor with pagination.
        [HttpGet("{sensorId}/{page}/{pageSize}")]
        public async Task<ActionResult<(int totalPages, IReadOnlyList<SensorData> readOnlyList)>> GetSensorsDataPaginationAsync(string sensorId,
                                                                                                                  int page,
                                                                                                                  int pageSize)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(sensorId))
                {
                    throw new ArgumentNullException(nameof(sensorId));
                }
                else if (page <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(page));
                }
                else if (pageSize <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(pageSize), "The page size must be greater than 0.");
                }
                else
                {
                    var (totalPages, readOnlyList) = await _sensorDataService.GetSensorsDataPaginationAsync(sensorId, page, pageSize);

                    if (readOnlyList == null)
                    {
                        throw new Exception("The list of SensorData is not found.");
                    }
                    else
                    {
                        return (totalPages, readOnlyList);
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        // Delete all SensorData of the specific sensor.
        [HttpDelete("sensorId")]
        public async Task<ActionResult<DeleteResult>> DeleteAllSensorDataBySensorIdAsync(string sensorId)
        {
            try
            {
                if (string.IsNullOrEmpty(sensorId))
                {
                    throw new ArgumentNullException(nameof(sensorId));
                }
                else
                {
                    var result = await _sensorDataService.DeleteAllSensorDataBySensorIdAsync(sensorId);

                    if (result == null)
                    {
                        throw new Exception($"Could not delete SensorData for the sensor with the {sensorId} Id.");
                    }
                    else
                    {
                        return result;
                    }
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
