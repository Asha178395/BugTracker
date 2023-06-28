namespace WebAPIComments.models
{
    public class NewComment
    {
        public int CommentId { get; set; }

        public string Comment1 { get; set; } = null!;

        public int? EmpId { get; set; }

        public string? IssueId { get; set; }

        public DateTime? CommentedOn { get; set; }

        public int? ParentCommentId { get; set; }
    }
}
