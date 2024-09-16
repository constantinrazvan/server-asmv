using AsmvBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public ProjectsController(ProjectsService service)
        {
            _service = service;
        }

        [HttpGet("all-projects")]
        public ActionResult<List<Project>> GetAllProjects()
        {
            var projects = _service.GetProjects();
            if (projects == null || !projects.Any())
            {
                return NotFound("No projects found.");
            }
            return Ok(projects);
        }

        [HttpGet("project/{id}")]
        public ActionResult<Project> GetProject(long id)
        {
            var project = _service.GetProject(id);
            if (project == null)
            {
                return NotFound($"Project with id {id} not found.");
            }
            return Ok(project);
        }

        [HttpPost("new-project")]
        public ActionResult<Project> NewProject([FromForm] ProjectDTO projectDto)
        {
            try
            {
                bool isAdded = _service.AddProject(projectDto);
                if (!isAdded)
                {
                    return BadRequest("Failed to add the project.");
                }
                return Ok("Project added successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update-project/{id}")]
        public ActionResult UpdateProject(long id, [FromForm] ProjectDTO projectDto)
        {
            try
            {
                bool isUpdated = _service.UpdateProject(id, projectDto);
                if (!isUpdated)
                {
                    return BadRequest("Failed to update the project.");
                }
                return Ok("Project updated successfully.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-project/{id}")]
        public ActionResult DeleteProject(long id)
        {
            bool isDeleted = _service.DeleteProject(id);
            if (!isDeleted)
            {
                return NotFound($"Project with id {id} not found.");
            }
            return Ok("Project deleted successfully.");
        }

        [HttpGet("projects-count")]
        public ActionResult<int> CountProjects()
        {
            int count = _service.Count();
            return Ok(count);
        }
    }
}