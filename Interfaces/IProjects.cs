using ServerAsmv.DTOs;
using AsmvBackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServerAsmv.Interfaces
{
    public interface IProjects
    {
        Task<bool> AddProject(ProjectDTO project);
        Task<bool> UpdateProject(long id, ProjectDTO project);
        Task<bool> DeleteProject(long id);
        Project? GetProject(long id);
        List<Project> GetProjects();
        int Count();
    }
}
