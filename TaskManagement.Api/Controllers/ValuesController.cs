using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TaskManagement.Data.UnitOfWork;
using TaskManagement.Dto.Value;
using TaskManagement.Model;
using TaskManagement.Service;

namespace TaskManagement.Api.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        private readonly IValueService _valueService;        
        private readonly ILogger _logger;
        public ValuesController(IValueService valueService , ILogger<ValuesController> logger)
        {
            _valueService = valueService;            
            _logger = logger;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetValues()
        {   
            try
            {
                var values = await _valueService.GetAllAsync();
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
                var value = await _valueService.GetByIdAsync(id);

                if (value == null)
                {
                    return NotFound();
                }

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

                if (await _valueService.IsDuplicateAsync(valueForCreateDto.Name))
                {
                    return BadRequest("Duplicate");
                }

                await _valueService.AddAsync(valueForCreateDto);
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
                if (!ModelState.IsValid || id!=valueForUpdateDto.Id)
                {
                    return BadRequest();
                }

                var selectedValue = await _valueService.GetByIdAsync(id);

                if (selectedValue == null)
                {
                    return NotFound();
                }

                _valueService.Update(valueForUpdateDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var selectedValue = await _valueService.GetByIdAsync(id);

                if (selectedValue == null)
                {
                    return NotFound();
                }

                var valueToDeleted = new ValueForDeleteDto
                {
                    Id = selectedValue.Id,
                    Name = selectedValue.Name
                };                

                var deletedValue = _valueService.Delete(valueToDeleted);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }
    }
}
