using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class InputProject
    {
        [Required]
       
        [RegularExpression("^[a-zA-Z0-9- _]{3,}$")]
        public string ProjectName { get; set; } = null!;

        [Required]
        [RegularExpression("^[a-zA-Z0-9- _]{1,}$")]

        public string ProjectId { get; set; } = null!;
    }
}
