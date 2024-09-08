using AsmvBackend.Models;
using ServerAsmv.DTOs;

namespace ServerAsmv.Interfaces {

    public interface IProjects {
        bool AddProject(ProjectDTO project);
        bool UpdateProject(long Id, ProjectDTO project);
        bool DeleteProject(long Id);
        Project GetProject(long Id);
        List<Project> GetProjects();
    }
}