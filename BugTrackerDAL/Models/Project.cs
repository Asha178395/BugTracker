using System;
using System.Collections.Generic;

namespace BugTrackerDAL.Models;

public partial class Project
{
    public string ProjectId { get; set; } = null!;

    public string ProjectName { get; set; } = null!;

    public int? TotalIssues { get; set; }

    public int? HighPriorityIssues { get; set; }

    public int? MediumPriorityIssues { get; set; }

    public int? LowPriorityIssues { get; set; }

    public int? S1seviourty { get; set; }

    public int? S2seviourty { get; set; }

    public int? S3seviourty { get; set; }

    public int? S4seviourty { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Issue> Issues { get; set; } = new List<Issue>();
}
