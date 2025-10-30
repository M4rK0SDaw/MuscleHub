using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MuscleHub.Data;
using MuscleHub.Models;
using MuscleHub.ViewModels;

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
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 10;
            var totalItems = await _context.Clases.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var clases = await _context.Clases
                .Include(c => c.Entrenadores)
                .OrderBy(c => c.Nombre)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var mv = clases.Select(c => new ClaseViewModel
            {
                ClaseId = c.ClaseId,
                Nombre = c.Nombre,
                Descripcion = c.Descripcion,
                Dia = c.Dia,
                HoraInicio = c.HoraInicio,
                HoraFin = c.HoraFin,
                EntrenadorId = c.EntrenadorId,
                Entrenador = new EntrenadoresViewModel
                {
                    EntrenadorId = c.Entrenadores?.EntrenadorId ?? 0,
                    Nombre = c.Entrenadores?.Nombre ?? "",
                    Apellido = c.Entrenadores?.Apellido ?? "",
                    Correo = c.Entrenadores?.Correo ?? "",
                    Telefono = c.Entrenadores?.Telefono ?? "",
                    Especialidad = c.Entrenadores?.Especialidad ?? "",
                    Estado = c.Entrenadores?.Estado ?? true,
                    FechaRegistro = c.Entrenadores?.FechaRegistro ?? DateTime.Now
                }
            }).ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(mv);
        }

        // GET: Clase/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var clase = await _context.Clases
                .Include(c => c.Entrenadores)
                .FirstOrDefaultAsync(m => m.ClaseId == id);

            if (clase == null) return NotFound();

            var vm = new ClaseViewModel
            {
                ClaseId = clase.ClaseId,
                Nombre = clase.Nombre,
                Descripcion = clase.Descripcion,
                Dia = clase.Dia,
                HoraInicio = clase.HoraInicio,
                HoraFin = clase.HoraFin,
                EntrenadorId = clase.EntrenadorId,
                Entrenador = new EntrenadoresViewModel
                {
                    EntrenadorId = clase.Entrenadores?.EntrenadorId ?? 0,
                    Nombre = clase.Entrenadores?.Nombre ?? "No asignado",
                    Apellido = clase.Entrenadores?.Apellido ?? "No asignado",
                    Especialidad = clase.Entrenadores?.Especialidad ?? "No asignada"
                }
            };

            return View(vm);
        }

        // GET: Clase/Create
        public async Task<IActionResult> Create()
        {
            await CargarEntrenadoresDropDown();
            return View();
        }

        // POST: Clase/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClaseViewModel vm)
        {
            if (ModelState.IsValid)
            {
                bool existeConflicto = _context.Clases.Any(c =>
                    c.EntrenadorId == vm.EntrenadorId &&
                    c.Dia == vm.Dia &&
                    ((vm.HoraInicio >= c.HoraInicio && vm.HoraInicio < c.HoraFin) ||
                     (vm.HoraFin > c.HoraInicio && vm.HoraFin <= c.HoraFin)));

                if (existeConflicto)
                {
                    ModelState.AddModelError("", "El entrenador ya tiene una clase en ese horario.");
                    await CargarEntrenadoresDropDown(vm.EntrenadorId);
                    return View(vm);
                }

                var model = new ClaseModels
                {
                    Nombre = vm.Nombre,
                    Descripcion = vm.Descripcion,
                    Dia = vm.Dia,
                    HoraInicio = vm.HoraInicio,
                    HoraFin = vm.HoraFin,
                    EntrenadorId = vm.EntrenadorId
                };

                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            await CargarEntrenadoresDropDown(vm.EntrenadorId);
            return View(vm);
        }

        // GET: Clase/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var clase = await _context.Clases
                .Include(c => c.Entrenadores)
                .FirstOrDefaultAsync(c => c.ClaseId == id);

            if (clase == null) return NotFound();

            var vm = new ClaseViewModel
            {
                ClaseId = clase.ClaseId,
                Nombre = clase.Nombre,
                Descripcion = clase.Descripcion,
                Dia = clase.Dia,
                HoraInicio = clase.HoraInicio,
                HoraFin = clase.HoraFin,
                EntrenadorId = clase.EntrenadorId,
                Entrenador = new EntrenadoresViewModel
                {
                    EntrenadorId = clase.Entrenadores?.EntrenadorId ?? 0,
                    Nombre = clase.Entrenadores?.Nombre ?? "No asignado",
                    Apellido = clase.Entrenadores?.Apellido ?? "No asignado",
                    Especialidad = clase.Entrenadores?.Especialidad ?? "No asignada"
                }
            };

            await CargarEntrenadoresDropDown(vm.EntrenadorId);
            return View(vm);
        }

        // POST: Clase/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ClaseViewModel vm)
        {
            if (id != vm.ClaseId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var model = await _context.Clases.FindAsync(id);
                    if (model == null) return NotFound();

                    model.Nombre = vm.Nombre;
                    model.Descripcion = vm.Descripcion;
                    model.Dia = vm.Dia;
                    model.HoraInicio = vm.HoraInicio;
                    model.HoraFin = vm.HoraFin;
                    model.EntrenadorId = vm.EntrenadorId;

                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClaseModelsExists(vm.ClaseId)) return NotFound();
                    else throw;
                }

                return RedirectToAction(nameof(Index));
            }

            await CargarEntrenadoresDropDown(vm.EntrenadorId);
            return View(vm);
        }

        // GET: Clase/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var clase = await _context.Clases
                .Include(c => c.Entrenadores)
                .FirstOrDefaultAsync(m => m.ClaseId == id);

            if (clase == null) return NotFound();

            var vm = new ClaseViewModel
            {
                ClaseId = clase.ClaseId,
                Nombre = clase.Nombre,
                Descripcion = clase.Descripcion,
                Dia = clase.Dia,
                HoraInicio = clase.HoraInicio,
                HoraFin = clase.HoraFin,
                EntrenadorId = clase.EntrenadorId,
                Entrenador = new EntrenadoresViewModel
                {
                    EntrenadorId = clase.Entrenadores?.EntrenadorId ?? 0,
                    Nombre = clase.Entrenadores?.Nombre ?? "No asignado",
                    Apellido = clase.Entrenadores?.Apellido ?? "No asignado",
                    Especialidad = clase.Entrenadores?.Especialidad ?? "No asignada"
                }
            };

            return View(vm);
        }

        // POST: Clase/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clase = await _context.Clases.FindAsync(id);
            if (clase != null)
            {
                _context.Clases.Remove(clase);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ClaseModelsExists(int id)
        {
            return _context.Clases.Any(e => e.ClaseId == id);
        }

        // Método auxiliar para cargar dropdown de entrenadores
        private async Task CargarEntrenadoresDropDown(int? selectedId = null)
        {
            var entrenadores = await _context.Entrenadores
                .Select(e => new
                {
                    e.EntrenadorId,
                    NombreCompleto = e.Nombre + " " + e.Apellido
                })
                .ToListAsync();

            ViewData["EntrenadorId"] = new SelectList(entrenadores, "EntrenadorId", "NombreCompleto", selectedId);
        }
    }
}
