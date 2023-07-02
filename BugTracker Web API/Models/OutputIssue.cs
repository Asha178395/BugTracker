using System.ComponentModel.DataAnnotations;

namespace BugTracker_Web_API.Models
{
    public class OutputIssue
    {
        [Required(ErrorMessage = "Issue Id is required")]
        public string IssueId { get; set; }
        [Required(ErrorMessage = "Project Id is required.")]
        public string ProjectId { get; set; }

        [Required(ErrorMessage = "Short Description is required.")]
        public string ShortDescription { get; set; } = null!;
        [Required(ErrorMessage = "Issue Type is required.")]
        [RegularExpression("^(Bug|Defect)$", ErrorMessage = "IssueType must be 'Bug' or 'Defect'.")]
        public string IssueType { get; set; } = null!;
        [Required(ErrorMessage = "Module Name is required.")]
        public string ModuleName { get; set; } = null!;
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; } = null!;


        [Required(ErrorMessage = "Priority is required.")]
        [RegularExpression("^(P1|P2|P3)$", ErrorMessage = "Priority must be 'P1' or 'P2' or 'P3'.")]
        public string Priority { get; set; } = null!;

        public DateTime? Targetdate { get; set; }

        

        public int AssignTo { get; set; }
        [Required(ErrorMessage = "Category is required.")]
        [RegularExpression("^(UI|API|Data Base)$", ErrorMessage = "IssueType must be 'UI' or 'API' or 'Data Base'.")]
        public string Category { get; set; } = null!;

        public string? Ressummary { get; set; }

        public string? StepsToReproduce { get; set; }
        [Required(ErrorMessage = "Testing Type is required.")]
        [RegularExpression("^(Smoke Testing|Regression Testing)$", ErrorMessage = "Priority must be 'Smoke Testing' or 'Regression Testing'.")]
        public string TestingType { get; set; } = null!;
        
        [Required(ErrorMessage = "Status is required.")]
        [RegularExpression("^(Open|Close|Hold|In Progress)$", ErrorMessage = "Priority must be 'Open' or 'Close' or 'In Progress' or 'Hold'.")]
        public string Status { get; set; } = null!;

       

        public string? Images { get; set; }
        [Required(ErrorMessage = "Seviority is required.")]
        [RegularExpression("^(S1|S2|S3|S4)$", ErrorMessage = "Seviority must be 'S1' or 'S2' or 'S3' or 'S4'.")]
        public string Seviority { get; set; } = null!;

        
        [Required(ErrorMessage = "Seviority is required.")]
        public int? Lastmodifydoneemp { get; set; }

        
    }
}
