using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TaskManagement.Data.DataContext;
using TaskManagement.Data.Repository;
using TaskManagement.Data.UnitOfWork;

namespace TaskManagement.Api.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        //private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public ValuesController(IUnitOfWork uow, ILogger<ValuesController> logger)
        {
            _uow = uow;
            //_mapper = mapper;
            _logger = logger;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetValues()
        {
            var values = await _uow.Values.GetAllTaskWithUser();

            return Ok(values);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetValue(int id)
        {
            var value = await _uow.Values.GetAsync(id);

            return Ok(value);
        }
        
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
        
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }
        
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
