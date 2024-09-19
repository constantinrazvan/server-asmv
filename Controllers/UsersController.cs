using ServerAsmv.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServerAsmv.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServerAsmv.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _service;
        private readonly ILogger<UsersController> _logger;

        public UsersController(UsersService service, ILogger<UsersController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("user/{id}")]
        public ActionResult<User> Get(long id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Get called with invalid ID: {Id}.", id);
                return BadRequest("Id cannot be 0 or lower than 0!");
            }

            var user = _service.GetUser(id);

            if (user == null)
            {
                _logger.LogWarning("User with ID {Id} not found.", id);
                return NotFound("User not found.");
            }

            _logger.LogInformation("User with ID {Id} retrieved successfully.", id);
            return Ok(user);
        }

        [HttpGet("all-users")]
        public ActionResult<List<User>> GetAll()
        {
            var users = _service.GetAll();
            _logger.LogInformation("Retrieved all users.");
            return Ok(users);
        }

        [HttpDelete("delete-user/{id}")]
        public async Task<ActionResult<bool>> DeleteUser(long id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("DeleteUser called with invalid ID: {Id}.", id);
                return BadRequest("Invalid ID.");
            }

            try
            {
                bool result = await _service.DeleteUser(id);
                _logger.LogInformation("User with ID {Id} deleted successfully.", id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting user with ID {Id}.", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPatch("update-email/{id}")]
        public async Task<ActionResult<bool>> ModifyEmail(long id, [FromBody] string email)
        {
            if (id <= 0)
            {
                _logger.LogWarning("ModifyEmail called with invalid user ID: {Id}.", id);
                return BadRequest("Invalid user ID. ID must be greater than 0.");
            }

            if (string.IsNullOrEmpty(email))
            {
                _logger.LogWarning("ModifyEmail called with empty email for user ID: {Id}.", id);
                return BadRequest("Email cannot be null or empty.");
            }

            try
            {
                bool updateResult = await _service.ModifyEmail(id, email);
                _logger.LogInformation("Email for user with ID {Id} updated successfully.", id);
                return Ok(updateResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating email for user with ID {Id}.", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPatch("update-password/{id}")]
        public async Task<ActionResult<bool>> ModifyPassword(long id, [FromBody] string password)
        {
            if (id <= 0)
            {
                _logger.LogWarning("ModifyPassword called with invalid user ID: {Id}.", id);
                return BadRequest("Invalid user ID. ID must be greater than 0.");
            }

            if (string.IsNullOrEmpty(password))
            {
                _logger.LogWarning("ModifyPassword called with empty password for user ID: {Id}.", id);
                return BadRequest("Password cannot be null or empty.");
            }

            try
            {
                bool updateResult = await _service.ModifyPassword(id, password);
                _logger.LogInformation("Password for user with ID {Id} updated successfully.", id);
                return Ok(updateResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating password for user with ID {Id}.", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("update-user/{id}")]
        public async Task<ActionResult<bool>> UpdateUser(long id, [FromBody] User userDto)
        {
            if (id <= 0)
            {
                _logger.LogWarning("UpdateUser called with invalid user ID: {Id}.", id);
                return BadRequest("Invalid user ID. ID must be greater than 0.");
            }

            if (userDto == null)
            {
                _logger.LogWarning("UpdateUser called with null User data for ID: {Id}.", id);
                return BadRequest("User data cannot be null.");
            }

            try
            {
                bool updateResult = await _service.UpdateUser(id, userDto);
                _logger.LogInformation("User with ID {Id} updated successfully.", id);
                return Ok(updateResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating user with ID {Id}.", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("users-count")]
        public ActionResult<int> Count()
        {
            try
            {
                int count = _service.Count();
                _logger.LogInformation("Retrieved users count: {Count}.", count);
                return Ok(count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while counting users.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}