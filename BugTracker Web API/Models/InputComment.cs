using System.ComponentModel.DataAnnotations;

namespace WebAPIComments.models
{
    public class InputComment
    {

        [Required]
        [RegularExpression("^[a-zA-Z0-9 _-]{2,}$")]
        public string Comment1 { get; set; }

        [Required]

        public string? IssueId { get; set; }

      
        [Required]
        public int? EmpId { get; set; }
        
        
        ///<summary>
        ///Can Be Null
        ///</summary>
       
        
        public int? ParentCommentId { get; set; }


    }
}