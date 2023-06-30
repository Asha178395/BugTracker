namespace IssueTracking_web_API.Models
{
    public class OutputProject
    {
        public string Projectid { get; set; } = null!;

        public string Projectname { get; set; } = null!;

        public int? TotalIssues { get; set; }

        public int? HighPriorityIssues { get; set; }

        public int? MediumPriorityIssues { get; set; }

        public int? LowPriorityIssues { get; set; }

        public int? S1seviourty { get; set; }

        public int? S2seviourty { get; set; }

        public int? S3seviourty { get; set; }

        public int? S4seviourty { get; set; }
    }
}
