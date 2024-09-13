using AsmvBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerAsmv.DTOs;
using ServerAsmv.Services;

namespace ServerAsmv.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _service;

        public AuthController(AuthService service)
        {
            this._service = service;
        }

        [AllowAnonymous]
        [HttpPost("/login")]
        public IActionResult Login([FromBody] LoginDTO user)
        {
            if (user == null)
            {
                return BadRequest("Invalid request: user is null");
            }

            string token = _service.login(user);

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Something happened!");
            }

            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost("/register")]
        public IActionResult Register([FromBody] RegisterDTO register)
        {
            if (register == null)
            {
                return BadRequest("Invalid request! User is null!");
            }

            bool result = _service.register(register);

            if (result)
            {
                return Ok("User created successfully!");
            }
            else
            {
                return BadRequest("User already exists!");
            }
        }

        public ActionResult<User> GetProfile(long Id) { 
            if(Id <= 0) {
                return null!;
            }

            return _service.GetProfile(Id);
        }
    }
}