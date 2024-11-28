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
        public async Task<IActionResult> AddVolunteer([FromForm] VolunteerDTO volunteer, IFormFile? photo)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ValidationProblemDetails(ModelState));
                }

                var result = await _service.AddVolunteer(volunteer, photo);

                if (result == "Volunteer added successfully!")
                {
                    return Ok(new { message = result });
                }

                return BadRequest(new { error = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An unexpected error occurred.",
                    details = ex.InnerException?.Message ?? ex.Message
                });
            }
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
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Validation Error: {error.ErrorMessage}");
                }
                return BadRequest(ModelState);
            }

            var success = await _service.UpdateVolunteer(id, volunteer, photo);
            if (success)
            {
                return Ok(true); 
            }

            return NotFound(false); 
        }


        // [HttpGet("{department}")]
        // public async Task<IActionResult> GetVolunteersByDepartment(string department)
        // {
        //     List<Volunteer> volunteers = await _service.SelectByDepartment(department); 
            
        //     if(volunteers.Any())
        //     {
        //         return Ok(volunteers);
        //     }

        //     return NotFound();
        // }
    }
}