using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MuscleHub.Data;
using MuscleHub.Models;
using MuscleHub.ViewModels;

namespace MuscleHub.Controllers
{
    public class EntrenadoresController : Controller
    {
        private readonly GymDbContext _context;

        public EntrenadoresController(GymDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 10;
            var totalEntrenadores = await _context.Entrenadores.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalEntrenadores / pageSize);
            var entrenadores = await _context.Entrenadores.OrderBy(e => e.Nombre).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            var vm = entrenadores.Select(e => new EntrenadoresViewModel
            {
                EntrenadorId = e.EntrenadorId,
                Nombre = e.Nombre,
                Apellido = e.Apellido,
                Correo = e.Correo,
                Telefono = e.Telefono,
                Especialidad = e.Especialidad,
                Estado = e.Estado,
                FechaRegistro = e.FechaRegistro
            }).ToList(); // Paginación
            ViewBag.CurrentPage = page; ViewBag.TotalPages = totalPages;
            return View(vm);
        }

        // GET: Entrenadores/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var entrenador = await _context.Entrenadores.FirstOrDefaultAsync(e => e.EntrenadorId == id);
            if (entrenador == null) return NotFound();

            var vm = new EntrenadoresViewModel
            {
                EntrenadorId = entrenador.EntrenadorId,
                Nombre = entrenador.Nombre,
                Apellido = entrenador.Apellido,
                Correo = entrenador.Correo,
                Telefono = entrenador.Telefono,
                Especialidad = entrenador.Especialidad,
                Estado = entrenador.Estado,
                FechaRegistro = entrenador.FechaRegistro
            };

            return View(vm);
        }

        // GET: Entrenadores/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Entrenadores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EntrenadoresViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var model = new EntrenadoresModels
                {
                    Nombre = vm.Nombre,
                    Apellido = vm.Apellido,
                    Correo = vm.Correo,
                    Telefono = vm.Telefono,
                    Especialidad = vm.Especialidad,
                    Password = vm.Password,
                    Estado = vm.Estado,
                    FechaRegistro = vm.FechaRegistro
                };

                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(vm);
        }

        // GET: Entrenadores/Edit/5
        [HttpGet]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Index)); // si no envían ID

            var entrenador = await _context.Entrenadores.FindAsync(id);
            if (entrenador == null) return NotFound();

            var vm = new EntrenadoresViewModel
            {
                EntrenadorId = entrenador.EntrenadorId,
                Nombre = entrenador.Nombre,
                Apellido = entrenador.Apellido,
                Correo = entrenador.Correo,
                Telefono = entrenador.Telefono,
                Especialidad = entrenador.Especialidad,
                Password = entrenador.Password,
                Estado = entrenador.Estado,
                FechaRegistro = entrenador.FechaRegistro
            };

            return View(vm);
        }

        // POST: Entrenadores/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EntrenadoresViewModel vm)
        {
            if (id != vm.EntrenadorId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var model = await _context.Entrenadores.FindAsync(id);
                    if (model == null) return NotFound();

                    model.Nombre = vm.Nombre;
                    model.Apellido = vm.Apellido;
                    model.Correo = vm.Correo;
                    model.Telefono = vm.Telefono;
                    model.Especialidad = vm.Especialidad;
                    model.Password = vm.Password;
                    model.Estado = vm.Estado;
                    model.FechaRegistro = vm.FechaRegistro;

                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EntrenadoresModelsExists(vm.EntrenadorId)) return NotFound();
                    else throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(vm);
        }

        // GET: Entrenadores/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var entrenador = await _context.Entrenadores.FirstOrDefaultAsync(e => e.EntrenadorId == id);
            if (entrenador == null) return NotFound();

            var vm = new EntrenadoresViewModel
            {
                EntrenadorId = entrenador.EntrenadorId,
                Nombre = entrenador.Nombre,
                Apellido = entrenador.Apellido,
                Correo = entrenador.Correo,
                Telefono = entrenador.Telefono,
                Especialidad = entrenador.Especialidad,
                Estado = entrenador.Estado,
                FechaRegistro = entrenador.FechaRegistro
            };

            return View(vm);
        }

        // POST: Entrenadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entrenador = await _context.Entrenadores.FindAsync(id);
            if (entrenador != null)
            {
                _context.Entrenadores.Remove(entrenador);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool EntrenadoresModelsExists(int id)
        {
            return _context.Entrenadores.Any(e => e.EntrenadorId == id);
        }
    }
}
