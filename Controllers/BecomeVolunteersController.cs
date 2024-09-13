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
    }
}
