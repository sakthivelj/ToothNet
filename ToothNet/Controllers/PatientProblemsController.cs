using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToothNet.Data;
using ToothNet.Models;

namespace ToothNet.Controllers
{
    [Authorize]
    public class PatientProblemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PatientProblemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PatientProblems
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var applicationDbContext = _context.PatientProblems.Where(b => b.ApplicationUserId == userId);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PatientProblems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PatientProblems == null)
            {
                return NotFound();
            }

            var patientProblem = await _context.PatientProblems
                .Include(p => p.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patientProblem == null)
            {
                return NotFound();
            }

            return View(patientProblem);
        }

        // GET: PatientProblems/Create
        public IActionResult Create()
        {
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            return View();
        }

        // POST: PatientProblems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Problem,Cure,Description,Price,Date,ApplicationUserId")] PatientProblem patientProblem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patientProblem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", patientProblem.ApplicationUserId);
            return View(patientProblem);
        }

        // GET: PatientProblems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PatientProblems == null)
            {
                return NotFound();
            }

            var patientProblem = await _context.PatientProblems.FindAsync(id);
            if (patientProblem == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", patientProblem.ApplicationUserId);
            return View(patientProblem);
        }

        // POST: PatientProblems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Problem,Cure,Description,Price,Date,ApplicationUserId")] PatientProblem patientProblem)
        {
            if (id != patientProblem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patientProblem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientProblemExists(patientProblem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", patientProblem.ApplicationUserId);
            return View(patientProblem);
        }

        // GET: PatientProblems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PatientProblems == null)
            {
                return NotFound();
            }

            var patientProblem = await _context.PatientProblems
                .Include(p => p.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patientProblem == null)
            {
                return NotFound();
            }

            return View(patientProblem);
        }

        // POST: PatientProblems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PatientProblems == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PatientProblems'  is null.");
            }
            var patientProblem = await _context.PatientProblems.FindAsync(id);
            if (patientProblem != null)
            {
                _context.PatientProblems.Remove(patientProblem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientProblemExists(int id)
        {
            return (_context.PatientProblems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
