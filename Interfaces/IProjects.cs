using ServerAsmv.DTOs;
using ServerAsmv.Models;
using Microsoft.AspNetCore.Http; // Asigură-te că adaugi această referință pentru IFormFile
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServerAsmv.Interfaces
{
    public interface IProjects
    {
        Task<bool> AddProject(ProjectDTO project, IFormFile photo); // Adaugă IFormFile pentru imagine
        Task<bool> UpdateProject(long id, ProjectDTO project, IFormFile photo);
        Task<bool> DeleteProject(long id);
        Project? GetProject(long id);
        List<Project> GetProjects();
        int Count();
    }
}
