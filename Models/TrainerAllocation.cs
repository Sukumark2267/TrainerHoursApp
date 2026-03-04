using System.ComponentModel.DataAnnotations;

namespace TrainerHoursApp.Models
{
    public class TrainerAllocation
    {
        public int Id { get; set; }

        [Required, StringLength(120)]
        public string TrainerName { get; set; } = "";

        [Required, StringLength(200)]
        public string TrainingTitle { get; set; } = "";

        [StringLength(200)]
        public string? Topic { get; set; }

        [Required, StringLength(50)]
        public string Branch { get; set; } = "";

        [Required, StringLength(10)]
        public string Section { get; set; } = "";

        [Required, StringLength(10)]
        public string Year { get; set; } = "";

        [StringLength(200)]
        public string? Location { get; set; }

        [StringLength(120)]
        public string? Instructor { get; set; }

        public decimal PlannedHours { get; set; }  // store what they entered (not date-wise)
        public bool IsActive { get; set; } = true;
    }
}
