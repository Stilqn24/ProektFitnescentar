using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FitnesCentar.Data;

namespace FitnesCentar.Controllers
{
    public class FitnesInstructorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FitnesInstructorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FitnesInstructors
        public async Task<IActionResult> Index()
        {
            return View(await _context.FitnesInstructors.ToListAsync());
        }

        // GET: FitnesInstructors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fitnesInstructor = await _context.FitnesInstructors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fitnesInstructor == null)
            {
                return NotFound();
            }

            return View(fitnesInstructor);
        }

        // GET: FitnesInstructors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FitnesInstructors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Description,ImageUrl,PhoneNumber,DateModified")] FitnesInstructor fitnesInstructor)
        {
            fitnesInstructor.DateModified = DateTime.Now;
            if (ModelState.IsValid)
            {
                return View(fitnesInstructor);
            }
            
            _context.Add(fitnesInstructor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: FitnesInstructors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fitnesInstructor = await _context.FitnesInstructors.FindAsync(id);
            if (fitnesInstructor == null)
            {
                return NotFound();
            }
            return View(fitnesInstructor);
        }

        // POST: FitnesInstructors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Description,ImageUrl,PhoneNumber,DateModified")] FitnesInstructor fitnesInstructor)
        {
            if (id != fitnesInstructor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fitnesInstructor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FitnesInstructorExists(fitnesInstructor.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        fitnesInstructor.DateModified = DateTime.Now;

                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(fitnesInstructor);
        }

        // GET: FitnesInstructors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fitnesInstructor = await _context.FitnesInstructors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fitnesInstructor == null)
            {
                return NotFound();
            }

            return View(fitnesInstructor);
        }

        // POST: FitnesInstructors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fitnesInstructor = await _context.FitnesInstructors.FindAsync(id);
            if (fitnesInstructor != null)
            {
                _context.FitnesInstructors.Remove(fitnesInstructor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FitnesInstructorExists(int id)
        {
            return _context.FitnesInstructors.Any(e => e.Id == id);
        }
    }
}
