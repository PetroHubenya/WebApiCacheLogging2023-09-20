using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class SensorDataService : ISensorDataService
    {
        private readonly IDataService _dataService;

        public SensorDataService(IDataService dataService)
        {
            _dataService = dataService;
        }
    }
}
