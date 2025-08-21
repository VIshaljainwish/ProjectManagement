using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Manager { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ICollection<EmployeeProjectLog> EmployeeProjectLogs { get; set; }
        = new List<EmployeeProjectLog>();
    }
}
