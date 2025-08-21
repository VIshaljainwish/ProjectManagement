using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Data;
using ProjectManagement.Models;
using ProjectManagement.Models.Dto;
using ProjectManagement.Repository.IRepository;

namespace ProjectManagement.Repository
{
    public class ProjectService : IProjectService
    {
        private readonly ApplicationDBContext _dBContext;
        private readonly IMapper _mapper;

        public ProjectService(ApplicationDBContext dBContext, IMapper mapper)
        {
            _dBContext = dBContext;
            _mapper = mapper;
        }

        public async Task<ProjectDto> InsertProject(ProjectDto projectDto)
        {
            var project = _mapper.Map<Project>(projectDto);
            await _dBContext.Projects.AddAsync(project);
            return _mapper.Map<ProjectDto>(project);
        }

        public async Task<List<ProjectDto>> ProjectList()
        {
            var projects = await _dBContext.Projects.ToListAsync();
            return _mapper.Map<List<ProjectDto>>(projects);
        }

        public async Task<List<ProjectReportDto>> GetProjectsAnalytics()
        {
            var projects = await _dBContext.Projects
                .Include(p => p.EmployeeProjectLogs) // navigation property required
                .ToListAsync();

            var result = projects.Select(p =>
            {
                var logs = p.EmployeeProjectLogs;

                double totalHours = logs.Sum(l => (double?)l.HoursLogged) ?? 0;
                int totalEmployees = logs.Select(l => l.EmployeeId).Distinct().Count();

                // Avoid divide by zero
                int totalDays = (int)((p.EndDate ?? DateTime.Now) - p.StartDate).TotalDays;
                totalDays = totalDays <= 0 ? 1 : totalDays;

                double avgDailyLogHours = Math.Round(totalHours / totalDays, 2);

                string status = (p.EndDate == null || p.EndDate >= DateTime.Now)
                    ? "On Track"
                    : "Delayed";

                return new ProjectReportDto
                {
                    ProjectName = p.Name,
                    TotalHoursLogged = totalHours,
                    TotalEmployees = totalEmployees,
                    AvgDailyLogHours = avgDailyLogHours,
                    ProjectStatus = status
                };
            }).ToList();

            return result;
        }

    }
}
