using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Models
{
    public class EmployeeProjectLog
    {
        [Key]
        public int LogId { get; set; }

        public int ProjectId { get; set; }   // FK
        public int EmployeeId { get; set; }
        public DateTime LogDate { get; set; }
        public int HoursLogged { get; set; }

        // Navigation property
        public Project Project { get; set; }
    }

}
