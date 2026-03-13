namespace TrainerHoursApp.Models
{
    public class TrainerDetailsViewModel
    {
        public string TrainerName { get; set; } = "";
        public string TrainingTitle { get; set; } = "";
        public string BranchesText { get; set; } = "";

        public decimal TotalPlannedHours { get; set; }
        public decimal TotalCompletedHours { get; set; }
        public decimal TotalPendingHours { get; set; }
        public decimal TotalMissedHours { get; set; }

        public decimal TotalExcessHours { get; set; }

        public int Year { get; set; }
        public int Month { get; set; }

        public List<TrainerDailyHour> DailyHours { get; set; } = new();
    }
}