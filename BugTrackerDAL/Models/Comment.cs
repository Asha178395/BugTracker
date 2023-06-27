using System;
using System.Collections.Generic;

namespace BugTrackerDAL.Models;

public partial class Comment
{
    public int CommentId { get; set; }

    public string? Comment1 { get; set; }

    public string? IssueId { get; set; }

    public int? EmpId { get; set; }

    public DateTime? CommentedOn { get; set; }

    public int? ParentCommentId { get; set; }

    public virtual Employee? Emp { get; set; }

    public virtual ICollection<Comment> InverseParentComment { get; set; } = new List<Comment>();

    public virtual Issue? Issue { get; set; }

    public virtual Comment? ParentComment { get; set; }
}
