using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Business.Interfaces;
using OnlineShopping.DataAccess.Entities;
using System.Threading.Tasks;

namespace OnlineShopping.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            var result = await _userService.RegisterAsync(user);
            return result ? Ok("User registered") : BadRequest();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User login)
        {
            var user = await _userService.LoginAsync(login.Username, login.Password);
            return user != null ? Ok(user) : Unauthorized();
        }
    }
}
