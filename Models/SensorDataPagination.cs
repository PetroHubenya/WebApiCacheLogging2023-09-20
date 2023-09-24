using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class SensorDataPagination
    {
        public List<SensorData>? SensorDataList { get; set; }

        public int NumberOfPages { get; set; }
    }
}
