using System.ComponentModel.DataAnnotations;



namespace BugTracker.Models
{
    public class inputemployee

    {
        [Required]
        [StringLength(20)]
        public string EmpName { get; set; } = null!;


        [Required]
        public string Projectid { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }
     }

}
