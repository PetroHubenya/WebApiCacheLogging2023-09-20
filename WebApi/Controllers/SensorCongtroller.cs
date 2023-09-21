using Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorCongtroller : ControllerBase
    {
        private readonly ISensorService _sensorService;

        public SensorCongtroller(ISensorService sensorService)
        {
            _sensorService = sensorService;
        }

        // GET: api/<SensorCongtroller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<SensorCongtroller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SensorCongtroller>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SensorCongtroller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SensorCongtroller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
