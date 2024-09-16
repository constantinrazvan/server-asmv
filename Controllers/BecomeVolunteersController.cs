using AsmvBackend.DTOs;
using AsmvBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerAsmv.Services;

namespace ServerAsmv.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BecomeVolunteersController : ControllerBase
    {
        private readonly BecomeVolunteerService _service;

        public BecomeVolunteersController(BecomeVolunteerService service) 
        { 
            this._service = service;
        }

        [HttpGet("volunteer/{id}")]
        public ActionResult<BecomeVolunteer> GetOne([FromRoute] long id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid ID.");
            }

            BecomeVolunteer found = _service.GetBecomeVolunteer(id);

            if (found == null)
            {
                return NotFound("Volunteer not found.");
            }

            return Ok(found);
        }

        [AllowAnonymous]
        [HttpPost("new-application")]
        public ActionResult<BecomeVolunteerDTO> AddOne([FromBody] BecomeVolunteerDTO req) 
        {
            if(req == null) 
            {
                return BadRequest("Request is null.");
            }

            _service.AddBecomeVolunteer(req);
            return req;
        }

        [HttpGet("all-volunteers")]
        public ActionResult<List<BecomeVolunteer>> GetAll() 
        {
            return _service.GetBecomeVolunteers();
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<bool>> Update([FromRoute] long id, [FromBody] BecomeVolunteerDTO becomeVolunteer)
        {
            if (becomeVolunteer == null)
            {
                return BadRequest("Request is null.");
            }

            bool updateResult = await _service.UpdateBecomeVolunteer(becomeVolunteer, id);
            return Ok(updateResult);
        }

        [HttpPut("mark-as-read/{id}")]
        public async Task<IActionResult> MarkAsRead(long id) 
        {
            if (id <= 0) 
            {
                return BadRequest("Id must be a positive integer.");
            }

            var resultMessage = await _service.MarkAsRead(id);
            
            if (resultMessage.Contains("not found"))
            {
                return NotFound(resultMessage);
            }

            return Ok(resultMessage);
        }
    }
}