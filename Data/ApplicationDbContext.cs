using Microsoft.EntityFrameworkCore;
using TrainerHoursApp.Models;

namespace TrainerHoursApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TrainerHour> TrainerHours { get; set; }
    }
}