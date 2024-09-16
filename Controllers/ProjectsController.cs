using AsmvBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServerAsmv.DTOs;
using ServerAsmv.Services;
using System.Collections.Generic;
using System.Linq;

namespace ServerAsmv.Controllers
{
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

        [HttpGet("project/{id}")]
        public ActionResult<Project> GetProject(long id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("GetProject called with invalid ID: {Id}.", id);
                return BadRequest("Invalid ID.");
            }

            try
            {
                var project = _service.GetProject(id);
                if (project == null)
                {
                    _logger.LogWarning("Project with ID {Id} not found.", id);
                    return NotFound($"Project with id {id} not found.");
                }

                _logger.LogInformation("Project with ID {Id} retrieved successfully.", id);
                return Ok(project);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving project with ID {Id}.", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("new-project")]
        public ActionResult<Project> NewProject([FromForm] ProjectDTO projectDto)
        {
            if (projectDto == null)
            {
                _logger.LogWarning("NewProject called with null ProjectDTO.");
                return BadRequest("Project data is null.");
            }

            try
            {
                bool isAdded = _service.AddProject(projectDto);
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
        public ActionResult UpdateProject(long id, [FromForm] ProjectDTO projectDto)
        {
            if (projectDto == null)
            {
                _logger.LogWarning("UpdateProject called with null ProjectDTO for ID: {Id}.", id);
                return BadRequest("Project data is null.");
            }

            try
            {
                bool isUpdated = _service.UpdateProject(id, projectDto);
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
        public ActionResult DeleteProject(long id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("DeleteProject called with invalid ID: {Id}.", id);
                return BadRequest("Invalid ID.");
            }

            try
            {
                bool isDeleted = _service.DeleteProject(id);
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

        [HttpGet("projects-count")]
        public ActionResult<int> CountProjects()
        {
            try
            {
                int count = _service.Count();
                _logger.LogInformation("Retrieved project count: {Count}.", count);
                return Ok(count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while counting projects.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}