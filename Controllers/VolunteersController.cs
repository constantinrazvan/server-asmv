using Microsoft.AspNetCore.Mvc;
using ServerAsmv.DTOs;
using ServerAsmv.Services;
using ServerAsmv.Models;
using Microsoft.AspNetCore.Authorization;

namespace ServerAsmv.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VolunteersController : ControllerBase
    {
        private readonly VolunteersService _service;

        public VolunteersController(VolunteersService service)
        {
            _service = service;
        }

        [HttpPost("/new-volunteer")]
        public ActionResult AddVolunteer([FromBody] VolunteerDTO volunteer)
        {
            if (volunteer == null)
            {
                return BadRequest("Volunteer cannot be null!");
            }

            try
            {
                _service.AddVolunteer(volunteer);
                return Ok("Volunteer added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Something went wrong!");
            }
        }

        [HttpGet("/volunteers")]
        public ActionResult<List<Volunteer>> GetVolunteers()
        {
            try
            {
                var volunteers = _service.GetVolunteers();
                return Ok(volunteers);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpGet("/volunteer/{id}")]
        public ActionResult<Volunteer> GetVolunteerById(long id)
        {
            if (id <= 0)
            {
                return BadRequest("Id must be greater than 0.");
            }

            try
            {
                var volunteer = _service.GetVolunteerById(id);
                return Ok(volunteer);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Volunteer not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpPut("/update-volunteer/{id}")]
        public ActionResult UpdateVolunteer(long id, [FromBody] VolunteerDTO volunteerDto)
        {
            if (id <= 0 || volunteerDto == null)
            {
                return BadRequest("Invalid input.");
            }

            try
            {
                var volunteer = new Volunteer
                {
                    Firstname = volunteerDto.Firstname,
                    Lastname = volunteerDto.Lastname,
                    Email = volunteerDto.Email,
                    City = volunteerDto.City,
                    Status = volunteerDto.Status,
                    JoinedDate = volunteerDto.JoinedDate
                };

                _service.UpdateVolunteer(id, volunteer);
                return Ok("Volunteer updated successfully.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Volunteer not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        [HttpDelete("/delete-volunteer/{id}")]
        public ActionResult DeleteVolunteer(long id)
        {
            if (id <= 0)
            {
                return BadRequest("Id must be greater than 0.");
            }

            try
            {
                _service.DeleteVolunteer(id);
                return Ok("Volunteer deleted successfully.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Volunteer not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
    }
}
