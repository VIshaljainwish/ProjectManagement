using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Data;
using ProjectManagement.Models;
using ProjectManagement.Models.Dto;
using ProjectManagement.Repository.IRepository;

namespace ProjectManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectMgntController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ResponseDto _response;

        public ProjectMgntController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _response = new ResponseDto();
        }

        [HttpGet]
        public async Task<ResponseDto> GetProjects()
        {
            try
            {
                var projects = await _unitOfWork.ProjectService.ProjectList();
                _response.Result = projects;
                _response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                _response.Result = null;
                _response.Message = $"Error fetching projects: {ex.Message}";
            }

            return _response;
        }

        [HttpPost]
        public async Task<ResponseDto> CreateProject([FromBody] ProjectDto project)
        {
            if (project == null)
            {
                _response.Message = "Data not found.";
                return _response;
            }

            if (project.EndDate.HasValue && project.EndDate < project.StartDate)
            {
                _response.Message = "End date must be greater than start date.";
                return _response;
            }

            try
            {
                var proj = await _unitOfWork.ProjectService.InsertProject(project);
                if (proj == null)
                {
                    _response.Message = "Data not inserted.";
                }
                else
                {
                    await _unitOfWork.SaveAsync();
                    _response.IsSuccess = true;
                    _response.Result = proj;
                    _response.Message = "Project created successfully.";
                }
            }
            catch (Exception ex)
            {
                _response.Message = $"Error inserting project: {ex.Message}";
            }

            return _response;
        }

        [HttpGet("analytics")]
        public async Task<ResponseDto> GetProjectsAnalytics()
        {
            try
            {
                var analytics = await _unitOfWork.ProjectService.GetProjectsAnalytics();
                return new ResponseDto
                {
                    IsSuccess = true,
                    Message = "Projects analytics fetched successfully",
                    Result = analytics
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto
                {
                    Message = "Error fetching project analytics"
                };
            }
        }

    }

}
