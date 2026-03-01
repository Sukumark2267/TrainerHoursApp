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

        // Helper to calculate pending/excess
        private void CalculatePendingAndExcess(TrainerHour th)
        {
            if (th.Hours > th.TotalHours)
            {
                th.ExcessHours = th.Hours - th.TotalHours;
                th.PendingHours = 0;
            }
            else
            {
                th.PendingHours = th.TotalHours - th.Hours;
                th.ExcessHours = 0;
            }
        }

        // GET: TrainerHours
        public async Task<IActionResult> Index()
        {
            var list = await _context.TrainerHours
                .OrderByDescending(t => t.Date)
                .ToListAsync();

            return View(list);
        }

        // POST: TrainerHours/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( string name, string branch,string batch, DateTime date, decimal hours, decimal totalHours, string? notes)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(branch))
            {
                // simple validation
                TempData["Error"] = "Name and Branch are required.";
                return RedirectToAction(nameof(Index));
            }

            var th = new TrainerHour
            {
                Name = name,
                Branch = branch,
                Batch = batch,
                Date = date,
                Hours = hours,
                TotalHours = totalHours,
                Notes = notes
            };

            CalculatePendingAndExcess(th);

            _context.Add(th);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: TrainerHours/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var th = await _context.TrainerHours.FindAsync(id);
            if (th == null) return NotFound();
            return View(th);
        }

        // POST: TrainerHours/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TrainerHour model)
        {
            if (id != model.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            CalculatePendingAndExcess(model);

            _context.Update(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: TrainerHours/Delete/5
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

            var totalHours = records.Sum(r => r.Hours);
            var totalTarget = records.Sum(r => r.TotalHours);

            var viewModel = new TrainerDetailsViewModel
            {
                Name = name,
                Records = records,
                TotalHours = totalHours,
                TotalTargetHours = totalTarget,
                TotalPending = totalHours < totalTarget ? totalTarget - totalHours : 0,
                TotalExcess = totalHours > totalTarget ? totalHours - totalTarget : 0
            };

            return PartialView("_TrainerDetails", viewModel);
        }
    }
}