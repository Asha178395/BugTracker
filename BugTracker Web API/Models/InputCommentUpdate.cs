using System.ComponentModel.DataAnnotations;

namespace IssueTracking_Web_API.Models
{
    public class InputCommentUpdate
    {
        [Required]
        public int CommentId { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z0-9 _-]{2,}$")]
        public string? Comment1 { get; set; }

    }
}
