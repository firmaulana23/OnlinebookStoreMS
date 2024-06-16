using Microsoft.AspNetCore.Mvc;
using OnlineBookstoreMS.Models.Entity;
using OnlineBookstoreMS.RequestSchema;
using OnlineBookstoreMS.Interface;

namespace OnlineBookstoreMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRequest req)
        {
            var user = new User
            {
                Username = req.Username,
                Email = req.Email,
                Role = "Customer"
            };

            if (req.Password is null || req.Password is ""){
                return BadRequest(new { message = "Cannot be Null" });
            }
            
            try
            {
                await _userService.Register(user, req.Password);
                return Ok();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest req)
        {
            var user = await _userService.Authenticate(req.Username, req.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserRequest req)
        {
            var user = await _userService.GetById(id);
            if (user == null)
                return NotFound();

            user.Username = req.Username;
            user.Email = req.Email;

            await _userService.Update(user, req.Password);
            return Ok();
        }
    }
}