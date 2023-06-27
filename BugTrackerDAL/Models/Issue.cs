using System;
using System.Collections.Generic;

namespace BugTrackerDAL.Models;

public partial class Issue
{
    public string IssueId { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public string ShortDescription { get; set; } = null!;

    public string IssueType { get; set; } = null!;

    public string ModuleName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Category { get; set; } = null!;

    public int Identfiedemp { get; set; }

    public DateTime Dateidentified { get; set; }

    public string Priority { get; set; } = null!;

    public DateTime? Targetdate { get; set; }

    public DateTime? Actualdate { get; set; }

    public int? AssignTo { get; set; }

    public string? Progress { get; set; }

    public string? Ressummary { get; set; }

    public string? StepsToReproduce { get; set; }

    public string TestingType { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string? LinkToParent { get; set; }

    public string? Images { get; set; }

    public int? IterationNumber { get; set; }

    public string Seviority { get; set; } = null!;

    public int? Lastmodifydoneemp { get; set; }

    public DateTime? Lastmodifydonedate { get; set; }

    public virtual Employee? AssignToNavigation { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Employee IdentfiedempNavigation { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;
}
