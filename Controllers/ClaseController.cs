using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MuscleHub.Data;
using MuscleHub.Models;

namespace MuscleHub.Controllers
{
    public class ClaseController : Controller
    {
        private readonly GymDbContext _context;

        public ClaseController(GymDbContext context)
        {
            _context = context;
        }

        // GET: Clase
        public async Task<IActionResult> Index()
        {
            var gymDbContext = _context.Clases.Include(c => c.Entrenadores);
            return View(await gymDbContext.ToListAsync());
        }

        // GET: Clase/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claseModels = await _context.Clases
                .Include(c => c.Entrenadores)
                .FirstOrDefaultAsync(m => m.ClaseId == id);
            if (claseModels == null)
            {
                return NotFound();
            }

            return View(claseModels);
        }

        // GET: Clase/Create
        public IActionResult Create()
        {
            ViewData["EntrenadorId"] = new SelectList(_context.Entrenadores, "EntrenadorId", "EntrenadorId");
            return View();
        }

        // POST: Clase/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClaseId,Nombre,Descripcion,Dia,HoraInicio,HoraFin,EntrenadorId")] ClaseModels claseModels)
        {
            if (ModelState.IsValid)
            {
                _context.Add(claseModels);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EntrenadorId"] = new SelectList(_context.Entrenadores, "EntrenadorId", "EntrenadorId", claseModels.EntrenadorId);
            return View(claseModels);
        }

        // GET: Clase/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claseModels = await _context.Clases.FindAsync(id);
            if (claseModels == null)
            {
                return NotFound();
            }
            ViewData["EntrenadorId"] = new SelectList(_context.Entrenadores, "EntrenadorId", "EntrenadorId", claseModels.EntrenadorId);
            return View(claseModels);
        }

        // POST: Clase/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClaseId,Nombre,Descripcion,Dia,HoraInicio,HoraFin,EntrenadorId")] ClaseModels claseModels)
        {
            if (id != claseModels.ClaseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(claseModels);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClaseModelsExists(claseModels.ClaseId))
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
            ViewData["EntrenadorId"] = new SelectList(_context.Entrenadores, "EntrenadorId", "EntrenadorId", claseModels.EntrenadorId);
            return View(claseModels);
        }

        // GET: Clase/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claseModels = await _context.Clases
                .Include(c => c.Entrenadores)
                .FirstOrDefaultAsync(m => m.ClaseId == id);
            if (claseModels == null)
            {
                return NotFound();
            }

            return View(claseModels);
        }

        // POST: Clase/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var claseModels = await _context.Clases.FindAsync(id);
            if (claseModels != null)
            {
                _context.Clases.Remove(claseModels);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClaseModelsExists(int id)
        {
            return _context.Clases.Any(e => e.ClaseId == id);
        }
    }
}
