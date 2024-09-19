using ServerAsmv.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServerAsmv.DTOs;
using ServerAsmv.Services;

namespace ServerAsmv.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly MessageService _service; 
        private readonly ILogger<MessageController> _logger;

        public MessageController(MessageService service, ILogger<MessageController> logger) 
        {
            _service = service;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("new-message")]
        public ActionResult<bool> Add([FromBody] MessageDTO message)
        {
            if (message == null)
            {
                _logger.LogWarning("Add called with null message.");
                return BadRequest("Message is null!");
            }

            try
            {
                var newMessage = new Message
                {
                    Fullname = message.Fullname,
                    Email = message.Email,
                    Text = message.Text
                };

                bool result = _service.AddMessage(newMessage);
                _logger.LogInformation("New message added successfully: {@Message}.", newMessage);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new message.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("message/{id}")]
        public ActionResult<Message> GetOne(long id) 
        {
            if (id <= 0) 
            {
                _logger.LogWarning("GetOne called with invalid ID: {Id}.", id);
                return BadRequest("Invalid ID.");
            }

            try
            {
                var message = _service.GetMessageById(id);
                if (message == null)
                {
                    _logger.LogWarning("Message with ID {Id} not found.", id);
                    return NotFound("Message not found.");
                }

                _logger.LogInformation("Message with ID {Id} retrieved successfully.", id);
                return Ok(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving message with ID {Id}.", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("all-messages")]
        public ActionResult<List<Message>> GetAll()
        {
            try
            {
                // Obține toate mesajele direct
                var messages = _service.GetMessages().ToList();

                _logger.LogInformation("Retrieved all messages.");
                return Ok(messages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all messages.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPatch("markAsRead/{id:long}")]
        public async Task<ActionResult<bool>> MarkAsRead(long id) 
        {
            if (id <= 0)
            {
                return BadRequest("Invalid message ID.");
            }

            try 
            {
                var newRequestStatus = await _service.MarkAsReadAsync(id);
                
                return Ok(newRequestStatus); 
            } 
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("/countMessages")]
        public ActionResult<int> Count() {
            return Ok(_service.Count());
        }
    }
}