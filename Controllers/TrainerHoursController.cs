using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainerHoursApp.Data;
using TrainerHoursApp.Models;

namespace TrainerHoursApp.Controllers
{
    public class TrainerHoursController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrainerHoursController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ Pending/Excess should use PlannedHours (NOT TotalHours)
        private void CalculatePendingAndExcess(TrainerHour th)
        {
            var planned = th.PlannedHours;

            if (th.Hours > planned)
            {
                th.ExcessHours = th.Hours - planned;
                th.PendingHours = 0;
            }
            else
            {
                th.PendingHours = planned - th.Hours;
                th.ExcessHours = 0;
            }
        }

        // ✅ Dashboard
        public async Task<IActionResult> Index()
        {
            var list = await _context.TrainerAllocations
        .OrderBy(t => t.TrainerName)
        .ThenBy(t => t.TrainingTitle)
        .ToListAsync();

            return View(list);
        }

        // ✅ Add Trainer Page
        [HttpGet]
        public IActionResult AddTrainer()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string name, string trainingTitle,string topic, DateTime date, string startTime,string endTime,string location,
                                                string instructor, string branch, string section, string year,decimal plannedHours, string? notes)
        {
           

            var trainerName = name.Trim();
            var title = trainingTitle.Trim();
            var branchVal = branch.Trim();
            var sectionVal = section.Trim();
            var yearVal = year.Trim();

            // -----------------------------
            // 1) UPSERT TrainerAllocation (NO date based duplicates)
            // -----------------------------
            var allocation = await _context.TrainerAllocations.FirstOrDefaultAsync(a =>
                a.TrainerName == trainerName &&
                a.TrainingTitle == title &&
                a.Branch == branchVal &&
                a.Section == sectionVal &&
                a.Year == yearVal
            );

            if (allocation == null)
            {
                allocation = new TrainerAllocation
                {
                    TrainerName = trainerName,
                    TrainingTitle = title,
                    Topic = string.IsNullOrWhiteSpace(topic) ? null : topic.Trim(),
                    Branch = branchVal,
                    Section = sectionVal,
                    Year = yearVal,
                    Location = string.IsNullOrWhiteSpace(location) ? null : location.Trim(),
                    Instructor = string.IsNullOrWhiteSpace(instructor) ? null : instructor.Trim(),
                    PlannedHours = plannedHours > 0 ? plannedHours : 0
                };

                _context.TrainerAllocations.Add(allocation);
            }
            else
            {
                // optional: update info if user entered latest values
                allocation.Topic = string.IsNullOrWhiteSpace(topic) ? allocation.Topic : topic.Trim();
                allocation.Location = string.IsNullOrWhiteSpace(location) ? allocation.Location : location.Trim();
                allocation.Instructor = string.IsNullOrWhiteSpace(instructor) ? allocation.Instructor : instructor.Trim();

                // if they change plannedHours later, update it
                if (plannedHours > 0) allocation.PlannedHours = plannedHours;

                _context.TrainerAllocations.Update(allocation);
            }

            await _context.SaveChangesAsync();

            // -----------------------------
            // 2) UPSERT TrainerDailyHours (ONLY trainer+date unique)
            // -----------------------------
            var day = date.Date;

            var daily = await _context.TrainerDailyHours.FirstOrDefaultAsync(d =>
                d.TrainerName == trainerName && d.Date == day
            );

            if (daily == null)
            {
                daily = new TrainerDailyHour
                {
                    TrainerName = trainerName,
                    Date = day,

                    // For now, default NotEntered + 0 hours (later Edit page updates)
                    CompletedHours = 8m,
                    Status = "Present"
                };

                _context.TrainerDailyHours.Add(daily);
            }

            // update date info (time/notes) from the form
            daily.StartTime = string.IsNullOrWhiteSpace(startTime) ? daily.StartTime : startTime.Trim();
            daily.EndTime = string.IsNullOrWhiteSpace(endTime) ? daily.EndTime : endTime.Trim();
            daily.Notes = string.IsNullOrWhiteSpace(notes) ? daily.Notes : notes.Trim();

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // ✅ Edit Hours screen (your existing edit)
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var th = await _context.TrainerHours.FindAsync(id);
            if (th == null) return NotFound();
            return View(th);
        }

        // ✅ Update hours + notes etc
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TrainerHour model)
        {
            if (id != model.Id) return NotFound();

            if (!ModelState.IsValid)
                return View(model);

            // ✅ IMPORTANT: keep pending/excess correct based on PlannedHours
            CalculatePendingAndExcess(model);

            _context.Update(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // ✅ Delete record
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var th = await _context.TrainerHours.FindAsync(id);
            if (th != null)
            {
                _context.TrainerHours.Remove(th);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // ✅ Trainer Details popup (Partial view)
        public async Task<IActionResult> TrainerDetails(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Trainer name is required.");

            var records = await _context.TrainerHours
                .Where(t => t.Name == name)
                .OrderBy(t => t.Branch)
                .ThenByDescending(t => t.Date)
                .ToListAsync();

            if (!records.Any())
                return Content("No records found for this trainer.");

            var totalDone = records.Sum(r => r.Hours);
            var totalPlanned = records.Sum(r => r.PlannedHours);

            var viewModel = new TrainerDetailsViewModel
            {
                Name = name,
                Records = records,
                TotalHours = totalDone,
                TotalTargetHours = totalPlanned,
                TotalPending = totalDone < totalPlanned ? totalPlanned - totalDone : 0,
                TotalExcess = totalDone > totalPlanned ? totalDone - totalPlanned : 0
            };

            return PartialView("_TrainerDetails", viewModel);
        }
    }
}