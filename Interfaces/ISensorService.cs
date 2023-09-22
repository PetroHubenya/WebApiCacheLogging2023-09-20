using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface ISensorService
    {
        Task CreateSensorAsync(Sensor sensor);
        Task DeleteSensorAsync(string id);
        Task<List<Sensor>> GetAllSensorsAsync();
        Task<Sensor> GetSensorAsync(string id);
        Task UpdateSensorAsync(Sensor newSensor);
    }
}
