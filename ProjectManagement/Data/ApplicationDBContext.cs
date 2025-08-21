using Microsoft.EntityFrameworkCore;
using ProjectManagement.Models;

namespace ProjectManagement.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options): base(options)
        {
            
        }
        public DbSet<Project> Projects { get; set; }
        public DbSet<EmployeeProjectLog> EmployeeProjectLogs { get; set; }
    }
}
