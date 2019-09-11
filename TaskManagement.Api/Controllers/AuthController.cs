using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Data.UnitOfWork;
using TaskManagement.Dto.User;
using TaskManagement.Model;

namespace TaskManagement.Api.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public AuthController(IUnitOfWork uow, IConfiguration configuration, ILogger<AuthController> logger)
        {
            _uow = uow;
            _configuration = configuration;
            _logger = logger;
        }

        [Route("emailExist")]
        [HttpGet]
        public async Task<IActionResult> IsEmailExist(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    return BadRequest();
                }
                if (await _uow.Users.UserExists(email))
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

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
        {
            if (userForRegisterDto == null || string.IsNullOrEmpty(userForRegisterDto.Email))
            {
                return BadRequest();
            }

            try
            {
                userForRegisterDto.Email = userForRegisterDto.Email.ToLower();

                if (await _uow.Users.UserExists(userForRegisterDto.Email))
                {
                    return BadRequest("Email already exists");
                }

                var userToCreate = new User
                {
                    FirstName = userForRegisterDto.FirstName,
                    LastName = userForRegisterDto.LastName,
                    Email = userForRegisterDto.Email,
                    MobileNumber = userForRegisterDto.MobileNumber,
                    Address = userForRegisterDto.Address,
                    Role = userForRegisterDto.Role
                };

                var createdUser = await _uow.Users.Register(userToCreate, userForRegisterDto.Password);

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]UserForLoginDto userForLoginDto)
        {
            if (userForLoginDto == null || string.IsNullOrEmpty(userForLoginDto.Email))
            {
                return BadRequest();
            }

            try
            {
                var userFromRepo = await _uow.Users.Login(userForLoginDto.Email.ToLower(), userForLoginDto.Password);

                if (userFromRepo == null)
                {
                    return Unauthorized();
                }

                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                    new Claim(ClaimTypes.Name,userFromRepo.FirstName + " " + userFromRepo.LastName),
                    new Claim(ClaimTypes.Role,userFromRepo.Role)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

                var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = credential                   
                };

                var tokenHandler = new JwtSecurityTokenHandler();

                var token = tokenHandler.CreateToken(tokenDescriptor);

                return Ok(new
                {
                    token = tokenHandler.WriteToken(token),
                    id= userFromRepo.Id.ToString(),
                    role = userFromRepo.Role
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }
    }
}