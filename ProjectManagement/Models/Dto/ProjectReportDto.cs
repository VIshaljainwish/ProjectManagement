namespace ProjectManagement.Models.Dto
{
    public class ProjectReportDto
    {
        public string ProjectName { get; set; } = string.Empty;
        public double TotalHoursLogged { get; set; }
        public int TotalEmployees { get; set; }
        public double AvgDailyLogHours { get; set; }
        public string ProjectStatus { get; set; } = string.Empty;
    }
}
