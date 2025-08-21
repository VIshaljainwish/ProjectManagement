using AutoMapper;
using ProjectManagement.Models.Dto;

namespace ProjectManagement.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Project, ProjectDto>().ReverseMap();
        }
    }
}
