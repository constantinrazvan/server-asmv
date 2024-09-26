using ServerAsmv.DTOs;
using ServerAsmv.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServerAsmv.Services;

namespace ServerAsmv.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BecomeVolunteersController : ControllerBase
    {
        private readonly BecomeVolunteerService _service;
        private readonly ILogger<BecomeVolunteersController> _logger;

        public BecomeVolunteersController(BecomeVolunteerService service, ILogger<BecomeVolunteersController> logger) 
        { 
            _service = service;
            _logger = logger;
        }

        [HttpGet("volunteer/{id}")]
        public ActionResult<BecomeVolunteer> GetOne([FromRoute] long id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("GetOne called with invalid ID: {Id}.", id);
                return BadRequest("Invalid ID.");
            }

            var found = _service.GetBecomeVolunteer(id);

            if (found == null)
            {
                _logger.LogWarning("Volunteer with ID {Id} not found.", id);
                return NotFound("Volunteer not found.");
            }

            _logger.LogInformation("Volunteer with ID {Id} retrieved successfully.", id);
            return Ok(found);
        }

        [AllowAnonymous]
        [HttpPost("new-application")]
        public ActionResult AddOne([FromBody] BecomeVolunteerDTO req) 
        {
            if (req == null) 
            {
                _logger.LogWarning("AddOne called with null request.");
                return BadRequest("Request is null.");
            }

            try
            {
                _service.AddBecomeVolunteer(req); // Assuming this method does not return an ID
                _logger.LogInformation("New volunteer application added successfully.");
                return Ok(new { message = "Volunteer application submitted successfully."} ); // 201 Created
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new volunteer application.");
                return BadRequest("Internal server error");
            }
        }

        [HttpGet("all-volunteers")]
        public ActionResult<List<BecomeVolunteer>> GetAll() 
        {
            var volunteers = _service.GetBecomeVolunteers();
            _logger.LogInformation("Retrieved all volunteers.");
            return Ok(volunteers);
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<bool>> Update([FromRoute] long id, [FromBody] BecomeVolunteerDTO becomeVolunteer)
        {
            if (becomeVolunteer == null)
            {
                _logger.LogWarning("Update called with null request for ID: {Id}.", id);
                return BadRequest("Request is null.");
            }

            try
            {
                bool updateResult = await _service.UpdateBecomeVolunteer(becomeVolunteer, id);
                _logger.LogInformation("Volunteer with ID {Id} updated successfully.", id);
                return Ok(updateResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating volunteer with ID {Id}.", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPatch("mark-as-read/{id}")]
        public async Task<IActionResult> MarkAsRead([FromRoute] long id) 
        {
            if (id <= 0) 
            {
                _logger.LogWarning("MarkAsRead called with invalid ID: {Id}.", id);
                return BadRequest("Id must be a positive integer.");
            }

            try
            {
                var resultMessage = await _service.MarkAsRead(id);

                if (resultMessage.Contains("not found"))
                {
                    _logger.LogWarning("Volunteer with ID {Id} not found while marking as read.", id);
                    return NotFound(resultMessage);
                }

                _logger.LogInformation("Volunteer with ID {Id} marked as read successfully.", id);
                return Ok(resultMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while marking volunteer with ID {Id} as read.", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("/countRequests")]
        public int Count() { 
            return _service.Count();
        }

        [HttpDelete("delete/{id}")]
        public ActionResult<bool> Delete(long Id)
        {
            if(Id <= 0) { 
                return false; 
            }

            return _service.Delete(Id);
        }
    }
}