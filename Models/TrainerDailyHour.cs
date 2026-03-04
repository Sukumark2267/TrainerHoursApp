using System.ComponentModel.DataAnnotations;

namespace TrainerHoursApp.Models
{
    public class TrainerDailyHour
    {
        public int Id { get; set; }

        [Required, StringLength(120)]
        public string TrainerName { get; set; } = "";

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        // store time as string to keep simple (your dropdown values like "09:00 AM")
        [StringLength(20)]
        public string? StartTime { get; set; }

        [StringLength(20)]
        public string? EndTime { get; set; }

        // For now: will be updated later in Edit page
        public decimal CompletedHours { get; set; } = 0m;

        // NotEntered / Present / Absent (later)
        [StringLength(20)]
        public string Status { get; set; } = "NotEntered";

        [StringLength(500)]
        public string? Notes { get; set; }
    }
}
