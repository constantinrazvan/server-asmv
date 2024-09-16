using AsmvBackend.Models;
using Microsoft.AspNetCore.Mvc;
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

        public UsersController(UsersService service) 
        {
            this._service = service;
        }

        [HttpGet("user/{id}")]
        public ActionResult<User> Get(long id) 
        {
            if (id <= 0)
            {
                return BadRequest("Id cannot be 0 or lower than 0!");
            }

            User user = _service.GetUser(id);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(user);
        }

        [HttpGet("all-users")]
        public ActionResult<List<User>> GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpDelete("delete-user/{id}")]
        public async Task<ActionResult<bool>> DeleteUser(long id) 
        {
            if (id <= 0) 
            {
                return BadRequest("Invalid ID.");
            }

            bool result = await _service.DeleteUser(id);
            return Ok(result);
        }

        [HttpPatch("update-email/{id}")]
        public async Task<ActionResult<bool>> ModifyEmail(long id, [FromBody] string email)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid user ID. ID must be greater than 0.");
            }

            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email cannot be null or empty.");
            }

            bool updateResult = await _service.ModifyEmail(id, email);
            return Ok(updateResult);
        }

        [HttpPatch("update-password/{id}")]
        public async Task<ActionResult<bool>> ModifyPassword(long id, [FromBody] string password)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid user ID. ID must be greater than 0.");
            }

            if (string.IsNullOrEmpty(password))
            {
                return BadRequest("Password cannot be null or empty.");
            }

            bool updateResult = await _service.ModifyPassword(id, password);
            return Ok(updateResult);
        }

        [HttpPut("update-user/{id}")]
        public async Task<ActionResult<bool>> UpdateUser(long id, [FromBody] User userDto)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid user ID. ID must be greater than 0.");
            }

            if (userDto == null)
            {
                return BadRequest("User data cannot be null.");
            }

            bool updateResult = await _service.UpdateUser(id, userDto);
            return Ok(updateResult);
        }

        [HttpGet("users-count")]
        public ActionResult<int> Count()
        {
            return Ok(_service.Count());
        }
    }
}
