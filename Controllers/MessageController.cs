using AsmvBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        public MessageController(MessageService service) {
            this._service = service;
        }

        [AllowAnonymous]
        [HttpPost("/new")]
        public ActionResult<bool> Add([FromBody] MessageDTO message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("Message is null!");
            }
        
            Message newMessage = new Message();
        
            newMessage.Fullname = message.Fullname;
            newMessage.Email = message.Email;
            newMessage.Text = message.Text;
        
            return _service.AddMessage(newMessage);
        }

        [HttpGet("/get/{id}")]
        public ActionResult<Message> GetOne(long Id) {
            if(Id <= 0) {
                return null!;
            }

            return _service.GetMessageById(Id);
        }

        [HttpGet("/all")]
        public ActionResult<Stack<Message>> GetAll() {
            return _service.GetMessages();
        }
    }
}
