using System;
using System.ComponentModel.DataAnnotations;

namespace TrainerHoursApp.Models
{
    public class TrainerHour
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Branch { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        // Hours done on that date
        [Display(Name = "No. of Hours")]
        public decimal Hours { get; set; }

        // Target / Total hours for that day
        [Display(Name = "Total No. of Hours")]
        public decimal TotalHours { get; set; }

        [Display(Name = "Pending Hours")]
        public decimal PendingHours { get; set; }

        [Display(Name = "Excess Hours")]
        public decimal ExcessHours { get; set; }

        [StringLength(50)]
        public string? Batch { get; set; }
        [StringLength(500)]
        public string? Notes { get; set; }
    }
}