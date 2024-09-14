using AsmvBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ServerAsmv.Services;

namespace ServerAsmv.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _service; 

        public UsersController(UsersService _service) {
            this._service = _service;
        }

        [HttpGet("/{id}")]
        public ActionResult<User> Get(long Id)
        {
            if(Id <= 0) {
                return BadRequest("Id cannot be 0 or lower than 0!");
            }

            return Ok(_service.GetUser(Id));
        }

        [HttpGet("/all")]
        public ActionResult<List<User>> GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpDelete("/{id}")]
        public ActionResult<Task<bool>> DeleteUser(long Id) 
        {
            if(Id <= 0) {
                return BadRequest();
            }

            return Ok(_service.DeleteUser(Id));
        }

        [HttpPatch("/email/{id}")]
        public async Task<ActionResult<string>> ModifyEmail(long id, [FromBody] string email)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid user ID. ID must be greater than 0.");
            }
        
            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest("Email is required and cannot be empty.");
            }
        
            bool modifiedEmail = await _service.ModifyEmail(id, email);
            return Ok(modifiedEmail);
        }

        [HttpPatch("/password/P{id}")]
        public async Task<ActionResult<bool>> ModifyPassword(long Id, [FromBody] string password) 
        {
            if(Id <= 0) {
                return BadRequest("Invalid user ID. ID must be grater than 0.");
            }

            if(string.IsNullOrWhiteSpace(password))
            {
                return BadRequest("Password is required and cannot be empty.");
            }

            bool modifiedPassword = await _service.ModifyPassword(Id, password);
            return Ok(modifiedPassword);
        }

        [HttpGet("/count")]
        public int Count()
        {
            return _service.Count();
        }

        [HttpPut("/update/{id}")]
        public async Task<ActionResult<bool>> UpdateUser(long Id, [FromBody] User user)
        {
            if(Id <= 0) {
                return BadRequest("Invalid user ID. ID must be grater than 0.");
            }

            if(user == null) {
                return BadRequest("Invalid user! User must be filled and not empty!");
            }

            bool updatedUser = await _service.UpdateUser(Id, user);
            return Ok(updatedUser);
        }
    }
}