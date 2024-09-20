using ServerAsmv.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Logging;
using ServerAsmv.DTOs;
using ServerAsmv.Services;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAsmv.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly ProjectsService _service;
        private readonly ILogger<ProjectsController> _logger;

        public ProjectsController(ProjectsService service, ILogger<ProjectsController> logger)
        {
            _service = service;
            _logger = logger;
        }
        

        [HttpGet("all-projects")]
        public ActionResult<List<Project>> GetAllProjects()
        {
            try
            {
                var projects = _service.GetProjects();
                if (projects == null || !projects.Any())
                {
                    _logger.LogWarning("No projects found.");
                    return NotFound("No projects found.");
                }

                _logger.LogInformation("Retrieved all projects successfully.");
                return Ok(projects);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all projects.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("project/{id}/image")]
        public async Task<IActionResult> GetProjectImage(long id)
        {
            var project = _service.GetProject(id);
            if (project == null || string.IsNullOrEmpty(project.Image))
            {
                _logger.LogWarning("Project with ID {Id} or image not found.", id);
                return NotFound($"Project with id {id} or image not found.");
            }

            var imagePath = Path.Combine("UploadedImages", Path.GetFileName(project.Image));

            if (!System.IO.File.Exists(imagePath))
            {
                _logger.LogWarning("Image file not found at path {Path}.", imagePath);
                return NotFound("Image file not found.");
            }

            var imageFile = await System.IO.File.ReadAllBytesAsync(imagePath);
            
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(Path.GetExtension(imagePath), out var contentType))
            {
                contentType = "application/octet-stream";
            }

            return File(imageFile, contentType);
        }


        [HttpPost("new-project")]
        public async Task<ActionResult<Project>> NewProject([FromForm] ProjectDTO projectDto)
        {
            if (projectDto == null)
            {
                _logger.LogWarning("NewProject called with null ProjectDTO.");
                return BadRequest("Project data is null.");
            }

            try
            {
                var isAdded = await _service.AddProject(projectDto);
                if (!isAdded)
                {
                    _logger.LogWarning("Failed to add project: {@ProjectDto}.", projectDto);
                    return BadRequest("Failed to add the project.");
                }

                _logger.LogInformation("Project added successfully: {@ProjectDto}.", projectDto);
                return Ok("Project added successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new project.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("update-project/{id}")]
        public async Task<ActionResult> UpdateProject(long id, [FromForm] ProjectDTO projectDto)
        {
            if (projectDto == null)
            {
                _logger.LogWarning("UpdateProject called with null ProjectDTO for ID: {Id}.", id);
                return BadRequest("Project data is null.");
            }

            try
            {
                var isUpdated = await _service.UpdateProject(id, projectDto);
                if (!isUpdated)
                {
                    _logger.LogWarning("Failed to update project with ID {Id}.", id);
                    return BadRequest("Failed to update the project.");
                }

                _logger.LogInformation("Project with ID {Id} updated successfully.", id);
                return Ok("Project updated successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning("Project with ID {Id} not found for update.", id);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating project with ID {Id}.", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("delete-project/{id}")]
        public async Task<ActionResult> DeleteProject(long id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("DeleteProject called with invalid ID: {Id}.", id);
                return BadRequest("Invalid ID.");
            }

            try
            {
                var isDeleted = await _service.DeleteProject(id);
                if (!isDeleted)
                {
                    _logger.LogWarning("Project with ID {Id} not found for deletion.", id);
                    return NotFound($"Project with id {id} not found.");
                }

                _logger.LogInformation("Project with ID {Id} deleted successfully.", id);
                return Ok("Project deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting project with ID {Id}.", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("count-projects")]
        public int Count()
        {
            return _service.Count();
        }
    }
}