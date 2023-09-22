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
        Task<DeleteResult> DeleteBoxAsync(string id);
        Task<DeleteResult> DeleteSensorAsync(string id);
        Task<List<Box>> GetAllBoxesAsync();
        Task<List<Sensor>> GetAllSensorsAsync();
        Task<Box> GetBoxAsync(string id);
        Task<Sensor> GetSensorAsync(string id);
        Task<UpdateResult> UpdateBoxAsync(Box newBox);
        Task<UpdateResult> UpdateSensorAsync(Sensor newSensor);
    }
}
