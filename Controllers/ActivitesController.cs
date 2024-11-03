using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using ServerAsmv.Services;
using ServerAsmv.DTOs;
using ServerAsmv.Models;

namespace ServerAsmv.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly ActivityService _activityService;

        public ActivitiesController(ActivityService activityService)
        {
            _activityService = activityService;
        }

        [HttpPost]
        public async Task<ActionResult<Activity>> AddActivity([FromBody] ActivityDTO activityDto)
        {
            if (activityDto == null)
            {
                return BadRequest("Activity data is null.");
            }

            try
            {
                var activity = await _activityService.AddActivityAsync(activityDto);
                return CreatedAtAction(nameof(GetActivityById), new { id = activity.Id }, activity);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Activity>>> GetAllActivities()
        {
            var activities = await _activityService.GetAllActivitiesAsync();
            return Ok(activities);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<Activity>> GetActivityById(long id)
        {
            var activity = await _activityService.GetActivityByIdAsync(id);
            if (activity == null)
            {
                return NotFound($"Activity with ID {id} not found.");
            }

            return Ok(activity);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdateActivity(long id, [FromBody] ActivityDTO activityDto)
        {
            if (activityDto == null)
            {
                return BadRequest("Activity data is null.");
            }

            var isUpdated = await _activityService.UpdateActivityAsync(id, activityDto);
            if (!isUpdated)
            {
                return NotFound($"Activity with ID {id} not found.");
            }

            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteActivity(long id)
        {
            var isDeleted = await _activityService.DeleteActivityAsync(id);
            if (!isDeleted)
            {
                return NotFound($"Activity with ID {id} not found.");
            }

            return NoContent();
        }
    }
}
