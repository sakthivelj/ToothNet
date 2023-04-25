using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToothNet.Data;
using ToothNet.Models;

namespace ToothNet.Controllers
{
    public class PatientPhotoesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public PatientPhotoesController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: PatientPhotoes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PatientPhotos.Include(p => p.ApplicationUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PatientPhotoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PatientPhotos == null)
            {
                return NotFound();
            }

            var patientPhoto = await _context.PatientPhotos
                .Include(p => p.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patientPhoto == null)
            {
                return NotFound();
            }

            return View(patientPhoto);
        }

        // GET: PatientPhotoes/Create
        public IActionResult Create()
        {
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            return View();
        }

        // POST: PatientPhotoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,ImageFile,AddedIn,ApplicationUserId")] PatientPhoto patientPhoto)
        {
            if (ModelState.IsValid)
            {

                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(patientPhoto.ImageFile.FileName);
                string extension = Path.GetExtension(patientPhoto.ImageFile.FileName);
                patientPhoto.ImageName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/Image/", fileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await patientPhoto.ImageFile.CopyToAsync(fileStream);
                }

                _context.Add(patientPhoto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", patientPhoto.ApplicationUserId);
            return View(patientPhoto);
        }

        // GET: PatientPhotoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PatientPhotos == null)
            {
                return NotFound();
            }

            var patientPhoto = await _context.PatientPhotos.FindAsync(id);
            if (patientPhoto == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", patientPhoto.ApplicationUserId);
            return View(patientPhoto);
        }

        // POST: PatientPhotoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,ImageName,AddedIn,ApplicationUserId")] PatientPhoto patientPhoto)
        {
            if (id != patientPhoto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patientPhoto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientPhotoExists(patientPhoto.Id))
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
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", patientPhoto.ApplicationUserId);
            return View(patientPhoto);
        }

        // GET: PatientPhotoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PatientPhotos == null)
            {
                return NotFound();
            }

            var patientPhoto = await _context.PatientPhotos
                .Include(p => p.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patientPhoto == null)
            {
                return NotFound();
            }

            return View(patientPhoto);
        }

        // POST: PatientPhotoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PatientPhotos == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PatientPhotos'  is null.");
            }
            var patientPhoto = await _context.PatientPhotos.FindAsync(id);

            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "image", patientPhoto.ImageName);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            if (patientPhoto != null)
            {
                _context.PatientPhotos.Remove(patientPhoto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientPhotoExists(int id)
        {
            return (_context.PatientPhotos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
