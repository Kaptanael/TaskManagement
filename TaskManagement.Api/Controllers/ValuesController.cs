using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TaskManagement.Api.Dtos.Value;
using TaskManagement.Data.DataContext;
using TaskManagement.Data.Repository;
using TaskManagement.Data.UnitOfWork;
using TaskManagement.Model;

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
            try
            {
                var values = await _uow.Values.GetAllAsync();
                return Ok(values);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetValue(int id)
        {   
            try
            {
                var value = await _uow.Values.GetByIdAsync(id);
                return Ok(value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpPost("addValue")]        
        public async Task<IActionResult> Post([FromBody] ValueForCreateDto valueForCreateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                if (await _uow.Values.IsDuplicateAsync(valueForCreateDto.Name))
                {
                    return BadRequest("Value already exists");
                }

                var valueToCreate = new Value
                {
                    Name = valueForCreateDto.Name
                };

                var createdValue = await _uow.Values.AddAsync(valueToCreate);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ValueForUpdateDto valueForUpdateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var valueToUpdate = new Value
                {
                    Id = valueForUpdateDto.Id,
                    Name = valueForUpdateDto.Name
                };                

                if (await _uow.Values.IsDuplicateAsync(valueForUpdateDto.Name))
                {
                    return BadRequest("Value already exists");
                }

                var updatedValue = _uow.Values.Update(valueToUpdate);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }
        
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
