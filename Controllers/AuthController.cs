using ServerAsmv.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServerAsmv.DTOs;
using ServerAsmv.Services;
using Microsoft.EntityFrameworkCore;

namespace ServerAsmv.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _service;
        private readonly ILogger<AuthController> _logger;

        public AuthController(AuthService service, ILogger<AuthController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO user)
        {
            if (user == null)
            {
                _logger.LogWarning("Login attempt with null user.");
                return BadRequest("Invalid request: user is null");
            }

            try
            {
                string? token = _service.login(user);

                if (token == null)
                {
                    _logger.LogWarning("Login failed for user {UserId}.", user.Email); // Logging based on email
                    return BadRequest(new { message = "Invalid credentials!" }); // More structured response
                }

                _logger.LogInformation("User {UserId} logged in successfully.", user.Email);
                return Ok(token); // Return the token in a structured response
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during login.");
                return StatusCode(500, "Internal server error");
            }
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDTO register)
        {
            if (register == null)
            {
                _logger.LogWarning("Registration attempt with null register data.");
                return BadRequest(new { message = "Invalid request! User data cannot be null." });
            }

            try
            {
                bool result = _service.register(register);

                if (result)
                {
                    _logger.LogInformation("User {Email} registered successfully.", register.Email);
                    return Ok(new { message = "User created successfully!" });
                }
                else
                {
                    _logger.LogWarning("Registration failed. User {Email} already exists.", register.Email);
                    return Conflict(new { message = "User already exists!" }); // Use 409 Conflict for existing users
                }
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database error occurred during registration.");
                return StatusCode(500, new { message = "Database error. Please try again later." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred during registration.");
                return StatusCode(500, new { message = "Internal server error. Please try again later." });
            }
        }


        [HttpGet("profile/{id}")]
        public ActionResult<User> GetProfile(long id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Profile retrieval attempt with invalid user id: {UserId}.", id);
                return BadRequest("Invalid user id.");
            }

            try
            {
                var profile = _service.GetProfile(id);
                if (profile == null)
                {
                    _logger.LogWarning("User profile not found for user id: {UserId}.", id);
                    return NotFound("User not found.");
                }

                _logger.LogInformation("User profile retrieved successfully for user id: {UserId}.", id);
                return Ok(profile);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the user profile.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}