using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Models.Dto
{
    public class ProjectDto
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Manager { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
