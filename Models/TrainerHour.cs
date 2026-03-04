using System;
using System.ComponentModel.DataAnnotations;

namespace TrainerHoursApp.Models
{
    public class TrainerHour
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = "";

        [StringLength(200)]
        public string? TrainingTitle { get; set; }

        [StringLength(200)]
        public string? Topic { get; set; }

        [StringLength(200)]
        public string? Location { get; set; }

        [StringLength(100)]
        public string? Instructor { get; set; }

        [Required, StringLength(50)]
        public string Branch { get; set; } = "";

        [StringLength(10)]
        public string? Section { get; set; }

        [StringLength(10)]
        public string? Year { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        // Store as display strings to keep it simple (12-hour format)
        [StringLength(20)]
        public string? StartTime { get; set; }

        [StringLength(20)]
        public string? EndTime { get; set; }

        // ✅ NEW: Planned hours for allocation (not actual hours)
        public decimal PlannedHours { get; set; }

        // Existing fields (keep if you already have)
        public decimal Hours { get; set; }          // actual / completed hours
        public decimal TotalHours { get; set; }     // (if still used in your old logic)
        public decimal PendingHours { get; set; }
        public decimal ExcessHours { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }
    }
}