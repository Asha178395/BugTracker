using System;
using System.Collections.Generic;

namespace BugTrackerDAL.Models;

public partial class Employee
{
    public int EmpId { get; set; }

    public string EmpName { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Issue> IssueAssignToNavigations { get; set; } = new List<Issue>();

    public virtual ICollection<Issue> IssueIdentfiedempNavigations { get; set; } = new List<Issue>();

    public virtual Project Project { get; set; } = null!;
}
