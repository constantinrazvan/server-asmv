using AsmvBackend.DTOs;
using AsmvBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerAsmv.Services;

namespace ServerAsmv.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BecomeVolunteersController : ControllerBase
    {
        private readonly BecomeVolunteerService _service;

        public BecomeVolunteersController(BecomeVolunteerService service) { 
            this._service = service;
        }

        [HttpGet("/{id}")]
        public ActionResult<BecomeVolunteer> GetOne([FromBody] long Id)
        {
            if(Id == 0) {
                return null!;
            }

            BecomeVolunteer found = _service.GetBecomeVolunteer(Id);
            
            if(found == null) {
                return null!;
            }

            return found;
        }

        [HttpPost("/new")]
        public ActionResult<BecomeVolunteerDTO> AddOne([FromBody] BecomeVolunteerDTO req) {
            if(req == null) {
                return null!;
            }

            _service.AddBecomeVolunteer(req);
            return req;
        }

        [HttpGet("/all")]
        public ActionResult<List<BecomeVolunteer>> GetAll() 
        {
            return _service.GetBecomeVolunteers();
        }

        [HttpPut("/update/{id}")]
        public async Task<ActionResult<bool>> Update([FromRoute] long id, [FromBody] BecomeVolunteerDTO becomeVolunteer)
        {
            if (becomeVolunteer == null)
            {
                return null!;
            }

            bool updateResult = await _service.UpdateBecomeVolunteer(becomeVolunteer, id);
            return Ok(updateResult);
        }

        [HttpPut("/markAsRead/{id}")]
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
 