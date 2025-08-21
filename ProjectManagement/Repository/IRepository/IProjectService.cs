using ProjectManagement.Models;
using ProjectManagement.Models.Dto;

namespace ProjectManagement.Repository.IRepository
{
    public interface IProjectService
    {
        Task<List<ProjectDto>> ProjectList();
        Task<ProjectDto> InsertProject(ProjectDto project);
        Task<List<ProjectReportDto>> GetProjectsAnalytics();
    }
}
