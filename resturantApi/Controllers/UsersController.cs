using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using resturant.Application.Dtos;
using resturant.Application.Services.Base;


namespace resturantApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public UsersController(IUserServices userServices)
        {
            _userServices = userServices;
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto userDto)
        {
            bool userFound = _userServices.LoginApi(userDto);

            if (userFound)
            {
                return Ok(new
                {
                    message = "Login Successful"
                });
            }
            else
            {
                return Unauthorized(new
                {
                    message = "Invalid username or password"
                }); 
            }
        }


        [HttpPost("regester")]
        public IActionResult Regester([FromBody] regestirUserDto userDto)
        {

            if (string.IsNullOrWhiteSpace(userDto.Name) ||
                string.IsNullOrWhiteSpace(userDto.Email) ||
                string.IsNullOrWhiteSpace(userDto.Username) ||
                string.IsNullOrWhiteSpace(userDto.Password))
            {
                return BadRequest(new
                {
                    message = "All fields are required"
                });
            }

            if (userDto.Password.Length < 4)
            {
                return BadRequest(new
                {
                    message = "Password must be at least 4 characters"
                });
            }


            bool isRegistered = _userServices.regesterApi(userDto);

            if (isRegistered)
            {
                return Ok(new
                {
                    message = "Regester Successful"
                });
            }
            else
            {
                return Unauthorized(new
                {
                    message = "username or email is taken from user before"
                }); 
            }
        }
    }
}
