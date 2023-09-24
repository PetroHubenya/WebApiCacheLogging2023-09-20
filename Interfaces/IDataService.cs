using Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IDataService
    {
        Task<ReplaceOneResult> AddSensorToBoxAsync(string boxId, string sensorId);
        Task CreateBoxAsync(Box box);
        Task CreateSensorAsync(Sensor sensor);
        Task CreateSensorDataAsync(SensorData sensorData);
        Task<DeleteResult> DeleteAllSensorDataBySensorIdAsync(string sensorId);
        Task<DeleteResult> DeleteBoxAsync(string id);
        Task<DeleteResult> DeleteSensorAsync(string id);
        Task<List<Box>> GetAllBoxesAsync();
        Task<List<SensorData>> GetAllSensorDataAsync();
        Task<List<Sensor>> GetAllSensorsAsync();
        Task<Box> GetBoxAsync(string id);
        Task<Sensor> GetSensorAsync(string id);
        Task<List<SensorData>> GetSensorsDataBySensorIdAsync(string sensorId);
        SensorDataPagination GetSensorsDataPaginationAsync(string sensorId, int rows, int page);
        Task<UpdateResult> UpdateBoxAsync(string id, Box newBox);
        Task<ReplaceOneResult> UpdateSensorAsync(string id, Sensor newSensor);
    }
}
