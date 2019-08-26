using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagement.Api.Dtos;
using TaskManagement.Data.UnitOfWork;
using TaskManagement.Model;

namespace TaskManagement.Api.Controllers
{ 
    [Route("api/[controller]")]
    public class TasksController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public TasksController(IUnitOfWork uow, IMapper mapper, ILogger<TasksController> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("tasks")]
        public async Task<IActionResult> GetTasks()
        {
            try
            {
                var tasks = await _uow.Tasks.GetAllTaskWithUser();

                var tasksToReturn = _mapper.Map<IEnumerable<TaskForListDto>>(tasks);

                return Ok(tasksToReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("tasksByUser/{id}")]
        public async Task<IActionResult> GetTasksByUser(int id)
        {
            try
            {                
                var tasks = await _uow.Tasks.GetAllTaskByUserId(id);

                var tasksToReturn = _mapper.Map<List<TaskForListDto>>(tasks);

                return Ok(tasksToReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("tasks/{id}")]
        public async Task<IActionResult> GetTask(int id)
        {
            try
            {
                var task = await _uow.Tasks.GetTaskWithUser(id);

                var taskToReturn = _mapper.Map<TaskForListDto>(task);

                return Ok(taskToReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpPost("addTask")]
        public async Task<IActionResult> CreateTask([FromBody]TaskDto taskDto)
        {
            try
            {
                if (taskDto == null || string.IsNullOrEmpty(taskDto.Name))
                {
                    return BadRequest();
                }

                taskDto.Name = taskDto.Name.ToLower();

                if (await _uow.Tasks.IsExistTaskName(taskDto.Name))
                {
                    return BadRequest("Task name already exists");
                }

                var taskToCreate = new UserTask
                {
                    Name = taskDto.Name,
                    Description = taskDto.Description,
                    StartDate = taskDto.StartDate,
                    EndDate = taskDto.EndDate,
                    UserId = taskDto.UserId
                };

                var createdTask = await _uow.Tasks.AddAsync(taskToCreate);
                await _uow.SaveAsync();

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpPut("updateTask")]
        public async Task<IActionResult> UpdateTask([FromBody]TaskDto taskDto)
        {
            try
            {
                if (taskDto == null || string.IsNullOrEmpty(taskDto.Name) || string.IsNullOrEmpty(taskDto.OldName))
                {
                    return BadRequest();
                }

                taskDto.Name = taskDto.Name.ToLower();

                if ((taskDto.Name !=taskDto.OldName) && await _uow.Tasks.IsExistTaskName(taskDto.OldName, taskDto.Name))
                {
                    return BadRequest("Task name already exists");
                }

                var taskToUpdate = new UserTask
                {
                    Id= taskDto.Id,
                    Name = taskDto.Name,
                    Description = taskDto.Description,
                    StartDate = taskDto.StartDate,
                    EndDate = taskDto.EndDate,
                    UserId = taskDto.UserId
                };

                var createdTask = _uow.Tasks.Update(taskToUpdate);
                _uow.Save();

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpDelete("deleteTask/{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            try
            {
                var task = await _uow.Tasks.GetAsync(id);

                if (task == null)
                {
                    return NotFound();
                }
                
                _uow.Tasks.Delete(task);
                _uow.Save();

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [Route("taskNameExist")]
        [HttpGet]
        public async Task<IActionResult> IsTaskNameExist(string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                {
                    return BadRequest();
                }
                if (await _uow.Tasks.IsExistTaskName(name))
                {
                    return Ok(true);
                }
                return Ok(false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [Route("oldTaskNameExist")]
        [HttpGet]
        public async Task<IActionResult> IsTaskNameExist(string oldName, string name)
        {
            try
            {
                try
                {
                    if (string.IsNullOrEmpty(oldName) || string.IsNullOrEmpty(name))
                    {
                        return BadRequest();
                    }
                    if (await _uow.Tasks.IsExistTaskName(oldName, name))
                    {
                        return Ok(true);
                    }
                    return Ok(false);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    return StatusCode(500);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }
    }
}