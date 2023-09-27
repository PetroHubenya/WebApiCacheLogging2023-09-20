using Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface ISensorDataService
    {
        Task CreateSensorDataAsync(SensorData sensorData);
        Task<DeleteResult> DeleteAllSensorDataBySensorIdAsync(string sensorId);
        Task<List<SensorData>> GetSensorsDataBySensorIdAsync(string sensorId);
        Task<(int totalPages, IReadOnlyList<SensorData> readOnlyList)> GetSensorsDataPaginationAsync(string sensorId, int page, int pageSize);
    }
}
