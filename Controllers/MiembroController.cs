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
        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 10;
            var totalItems = await _context.Miembros.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var miembros = await _context.Miembros
                .OrderBy(m => m.MiembroId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var vm = miembros.Select(m => new MiembroViewModel
            {
                MiembroId = m.MiembroId,
                Nombre = m.Nombre,
                Apellido = m.Apellido,
                Correo = m.Correo,
                Password = m.Password,
                Telefono = m.Telefono,
                FechaNacimiento = m.FechaNacimiento,
                Estado = m.Estado,
                FechaRegistro = m.FechaRegistro
            }).ToList();

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            return View(vm);
        }

        // GET: Miembros/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var miembro = await _context.Miembros.FirstOrDefaultAsync(m => m.MiembroId == id);
            if (miembro == null) return NotFound();

            var vm = new MiembroViewModel
            {
                MiembroId = miembro.MiembroId,
                Nombre = miembro.Nombre,
                Apellido = miembro.Apellido,
                Correo = miembro.Correo,
                Password = miembro.Password,
                Telefono = miembro.Telefono,
                FechaNacimiento = miembro.FechaNacimiento,
                Estado = miembro.Estado,
                FechaRegistro = miembro.FechaRegistro
            };

            return View(vm);
        }

        // GET: Miembro/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Miembro/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MiembroViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var miembro = new MiembroModels
                {
                    Nombre = viewModel.Nombre,
                    Apellido = viewModel.Apellido,
                    Correo = viewModel.Correo,
                    Password = viewModel.Password,
                    Telefono = viewModel.Telefono,
                    FechaNacimiento = viewModel.FechaNacimiento,
                    Estado = viewModel.Estado,
                    FechaRegistro = DateTime.Now // o lo que necesites
                };

                _context.Add(miembro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        // GET: Miembro/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var miembro = await _context.Miembros.FindAsync(id);
            if (miembro == null) return NotFound();

            var viewModel = new MiembroViewModel
            {
                MiembroId = miembro.MiembroId,
                Nombre = miembro.Nombre,
                Apellido = miembro.Apellido,
                Correo = miembro.Correo,
                Password = miembro.Password,
                Telefono = miembro.Telefono,
                FechaNacimiento = miembro.FechaNacimiento,
                Estado = miembro.Estado,
                FechaRegistro = miembro.FechaRegistro
            };

            return View(viewModel);
        }

        // POST: Miembro/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MiembroViewModel viewModel)
        {
            if (id != viewModel.MiembroId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var miembro = await _context.Miembros.FindAsync(id);
                    if (miembro == null) return NotFound();

                    miembro.Nombre = viewModel.Nombre;
                    miembro.Apellido = viewModel.Apellido;
                    miembro.Correo = viewModel.Correo;
                    miembro.Password = viewModel.Password;
                    miembro.Telefono = viewModel.Telefono;
                    miembro.FechaNacimiento = viewModel.FechaNacimiento;
                    miembro.Estado = viewModel.Estado;
                    miembro.FechaRegistro = viewModel.FechaRegistro;

                    _context.Update(miembro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MiembroExists(viewModel.MiembroId)) return NotFound();
                    else throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        // GET: Miembro/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var miembro = await _context.Miembros.FirstOrDefaultAsync(m => m.MiembroId == id);
            if (miembro == null) return NotFound();

            var viewModel = new MiembroViewModel
            {
                MiembroId = miembro.MiembroId,
                Nombre = miembro.Nombre,
                Apellido = miembro.Apellido,
                Correo = miembro.Correo,
                Password = miembro.Password,
                Telefono = miembro.Telefono,
                FechaNacimiento = miembro.FechaNacimiento,
                Estado = miembro.Estado,
                FechaRegistro = miembro.FechaRegistro
            };

            return View(viewModel);
        }

        // POST: Miembro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var miembro = await _context.Miembros.FindAsync(id);
            if (miembro != null)
            {
                _context.Miembros.Remove(miembro);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool MiembroExists(int id)
        {
            return _context.Miembros.Any(e => e.MiembroId == id);
        }
    }
}