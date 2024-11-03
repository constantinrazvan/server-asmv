using Microsoft.AspNetCore.Mvc;
using ServerAsmv.DTOs;
using ServerAsmv.Models;
using ServerAsmv.Services;

namespace ServerAsmv.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VolunteersController : ControllerBase 
    {
        private readonly VolunteerService _service;

        public VolunteersController(VolunteerService service)
        {
            _service = service;
        }

        [HttpPost("new-volunteer")]
        public async Task<ActionResult<string>> AddVolunteer([FromForm] VolunteerDTO volunteer, IFormFile photo)
        {
            var result = await _service.AddVolunteer(volunteer, photo);

            if (result == "Volunteer added succesfully!")
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        // GET api/volunteers
        [HttpGet]
        public async Task<ActionResult<List<Volunteer>>> GetVolunteers()
        {
            var volunteers = await _service.GetVolunteers();
            return Ok(volunteers); 
        }

        // GET api/volunteers/{id}
        [HttpGet("{id}")]
        public ActionResult<Volunteer> GetVolunteer(long id)
        {
            Volunteer volunteer = _service.GetVolunteer(id);
            if (volunteer != null)
            {
                return Ok(volunteer); 
            }

            return NotFound($"Voluntarul cu id {id} nu a fost gasit!"); 
        }

        // DELETE api/volunteers/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveVolunteer(long id)
        {
            var (success, message) = await _service.RemoveVolunteer(id);
            if (success)
            {
                return NoContent();  // Return 204 No Content for successful deletion
            }

            return NotFound(message);
        }

        // PUT api/volunteers/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> UpdateVolunteer(long id, [FromForm] VolunteerDTO volunteer, IFormFile photo)
        {
            var success = await _service.UpdateVolunteer(id, volunteer, photo);
            if (success)
            {
                return Ok(true); 
            }

            return NotFound(false); 
        }
    }
}
