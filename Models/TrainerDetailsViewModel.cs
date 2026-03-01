namespace TrainerHoursApp.Models
{
    public class TrainerDetailsViewModel
    {
        public string Name { get; set; }

        public List<TrainerHour> Records { get; set; } = new();

        public decimal TotalHours { get; set; }
        public decimal TotalTargetHours { get; set; }
        public decimal TotalPending { get; set; }
        public decimal TotalExcess { get; set; }
    }
}