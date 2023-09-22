using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IBoxService
    {
        Task AddSensorToBoxAsync(string boxId, string sensorId);
        Task CreateBoxAsync(Box box);
        Task DeleteBoxAsync(string id);
        Task<List<Box>> GetAllBoxesAsync();
        Task<Box> GetBoxAsync(string id);
        Task UpdateBoxAsync(Box newBox);
    }
}
