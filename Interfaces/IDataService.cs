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
        Task<ReplaceOneResult> AddSensorToBox(string boxId, string sensorId);
        Task CreateBoxAsync(Box box);
        Task CreateSensorAsync(Sensor sensor);
        Task<DeleteResult> DeleteBox(string id);
        Task<DeleteResult> DeleteSensor(string id);
        Task<List<Box>> GetAllBoxesAsync();
        Task<List<Sensor>> GetAllSensorsAsync();
        Task<Box> GetBox(string id);
        Task<Sensor> GetSensor(string id);
        Task<UpdateResult> UpdateBox(Box newBox);
        Task<UpdateResult> UpdateSensor(Sensor newSensor);
    }
}
