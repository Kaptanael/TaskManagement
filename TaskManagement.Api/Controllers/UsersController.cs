using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagement.Data.UnitOfWork;
using TaskManagement.Dto.User;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManagement.Api.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public UsersController(IUnitOfWork uow, IMapper mapper, ILogger<UsersController> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await _uow.Users.GetAllAsync();

                var usersToReturn = _mapper.Map<IEnumerable<UserForListDto>>(users);

                return Ok(usersToReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }        

        [HttpGet("users/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            try
            {
                var user = await _uow.Users.GetByIdAsync(id);

                var userToReturn = _mapper.Map<UserForListDto>(user);

                return Ok(userToReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("usernames")]
        public async Task<IActionResult> GetUserNames()
        {
            try
            {
                var users = await _uow.Users.GetAllAsync();

                List<UserForNameDto> userNamesToReturn = new List<UserForNameDto>();

                if (users != null && users.Count() > 0)
                {
                    foreach (var user in users)
                    {
                        UserForNameDto userForNameDto = new UserForNameDto();
                        userForNameDto.UserId = user.Id;
                        userForNameDto.Name = user.FirstName + " " + user.LastName;
                        userNamesToReturn.Add(userForNameDto);
                    }
                }

                return Ok(userNamesToReturn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }           
    }
}
