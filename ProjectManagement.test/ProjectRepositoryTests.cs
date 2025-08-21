using Microsoft.EntityFrameworkCore;
using ProjectManagement.Data;
using ProjectManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.test
{
    public class ProjectRepositoryTests
    {
        private ApplicationDBContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: "ProjectTestDb")
                .Options;

            return new ApplicationDBContext(options);
        }

        [Fact]
        public async Task InsertProject_ShouldAddProject()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var project = new Project
            {
                Name = "Test Project",
                Manager = "John",
                StartDate = DateTime.Now
            };

            // Act
            await context.Projects.AddAsync(project);
            await context.SaveChangesAsync();

            // Assert
            Assert.Single(context.Projects);
            Assert.Equal("Test Project", (await context.Projects.FirstAsync()).Name);
        }

        [Fact]
        public async Task ProjectList_ShouldReturnAllProjects()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            context.Projects.Add(new Project { Name = "P1", Manager = "M1", StartDate = DateTime.Now });
            context.Projects.Add(new Project { Name = "P2", Manager = "M2", StartDate = DateTime.Now });
            await context.SaveChangesAsync();

            // Act
            var projects = await context.Projects.ToListAsync();

            // Assert
            Assert.Equal(2, projects.Count);
        }
    }
}
