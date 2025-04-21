using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MuscleHub.Data;
using MuscleHub.Models;
using MuscleHub.ViewModels;

namespace MuscleHub.Controllers
{
    public class MiembroController : Controller
    {
        private readonly GymDbContext _context;

        public MiembroController(GymDbContext context)
        {
            _context = context;
        }

        // GET: Miembros
        public async Task<IActionResult> Index()
        {
            var miembros = await _context.Miembros.ToListAsync();

            var viewModel = miembros.Select(m => new MiembroViewModel
            {
                MiembroId = m.MiembroId,
                Nombre = m.Nombre,
                Apellido = m.Apellido,
                Correo = m.Correo,
                Telefono = m.Telefono,
                FechaNacimiento = m.FechaNacimiento,
                Estado = m.Estado,
                FechaRegistro = m.FechaRegistro
            }).ToList();

            return View(viewModel);
        }

        // GET: Miembros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var miembroModels = await _context.Miembros
                .FirstOrDefaultAsync(m => m.MiembroId == id);

            if (miembroModels == null) return NotFound();

            return View(miembroModels);
        }

        // GET: Miembro/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Miembro/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MiembroId,Nombre,Apellido,Correo,Telefono,FechaNacimiento,Estado,FechaRegistro")] MiembroModels miembroModels)
        {
            if (ModelState.IsValid)
            {
                _context.Add(miembroModels);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(miembroModels);
        }

        // GET: Miembro/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var miembroModels = await _context.Miembros.FindAsync(id);
            if (miembroModels == null) return NotFound();

            return View(miembroModels);
        }

        // POST: Miembro/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MiembroId,Nombre,Apellido,Correo,Telefono,FechaNacimiento,Estado,FechaRegistro")] MiembroModels miembroModels)
        {
            if (id != miembroModels.MiembroId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(miembroModels);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MiembroModelsExists(miembroModels.MiembroId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(miembroModels);
        }

        // GET: Miembro/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var miembroModels = await _context.Miembros
                .FirstOrDefaultAsync(m => m.MiembroId == id);

            if (miembroModels == null) return NotFound();

            return View(miembroModels);
        }

        // POST: Miembro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var miembroModels = await _context.Miembros.FindAsync(id);
            if (miembroModels != null)
            {
                _context.Miembros.Remove(miembroModels);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MiembroModelsExists(int id)
        {
            return _context.Miembros.Any(e => e.MiembroId == id);
        }
    }
}
