using AsmvBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerAsmv.DTOs;
using ServerAsmv.Services;

namespace ServerAsmv.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly MessageService _service; 

        public MessageController(MessageService service) 
        {
            this._service = service;
        }

        [AllowAnonymous]
        [HttpPost("new-message")]
        public ActionResult<bool> Add([FromBody] MessageDTO message)
        {
            if (message == null)
            {
                return BadRequest("Message is null!");
            }
        
            Message newMessage = new Message
            {
                Fullname = message.Fullname,
                Email = message.Email,
                Text = message.Text
            };
        
            return _service.AddMessage(newMessage);
        }

        [HttpGet("message/{id}")]
        public ActionResult<Message> GetOne(long id) 
        {
            if (id <= 0) 
            {
                return BadRequest("Invalid ID.");
            }

            return _service.GetMessageById(id);
        }

        [HttpGet("all-messages")]
        public ActionResult<Stack<Message>> GetAll() 
        {
            return _service.GetMessages();
        }
    }
}