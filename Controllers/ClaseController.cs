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

        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 10;
            var totalItems = await _context.Clases.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            // Carga las clases con los entrenadores asociados
            var clases = await _context.Clases
                .Include(c => c.Entrenadores) // Cargar la relación de Entrenadores
                .OrderBy(c => c.Nombre)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Proyección de los datos a un ViewModel
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
                    EntrenadorId = c.Entrenadores?.EntrenadorId ?? 0, // Asigna el ID del entrenador
                    Nombre = c.Entrenadores?.Nombre ?? "",           // Asigna el nombre del entrenador
                    Apellido = c.Entrenadores?.Apellido ?? "",       // Asigna el apellido del entrenador
                    Correo = c.Entrenadores?.Correo ?? "",           // Asigna el correo del entrenador
                    Telefono = c.Entrenadores?.Telefono ?? "",       // Asigna el teléfono del entrenador
                    Especialidad = c.Entrenadores?.Especialidad ?? "", // Asigna la especialidad del entrenador
                    Estado = c.Entrenadores?.Estado ?? true,         // Asigna el estado del entrenador
                    FechaRegistro = c.Entrenadores?.FechaRegistro ?? DateTime.Now // Asigna la fecha de registro
                }
            }).ToList();

            // Pasa la información de la página actual y el total de páginas a la vista
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
                
            return View(mv);
        }



        // GET: Clase/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var clase = await _context.Clases
                .Include(c => c.Entrenadores)  // Cargar el Entrenador relacionado
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
                    // Asignar valores predeterminados si no hay un entrenador relacionado
                    EntrenadorId = clase.Entrenadores?.EntrenadorId ?? 0,
                    Nombre = clase.Entrenadores?.Nombre ?? "No asignado",
                    Apellido = clase.Entrenadores?.Apellido ?? "No asignado",
                    Especialidad = clase.Entrenadores?.Especialidad ?? "No asignada" // Asegúrate de tener la propiedad Especialidad en el ViewModel si es necesario
                }
            };

            return View(vm);
        }


        // GET: Clase/Create
        public IActionResult Create()
        {
            ViewData["EntrenadorId"] = new SelectList(_context.Entrenadores, "EntrenadorId", "NombreCompleto");

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
                    ViewData["EntrenadorId"] = new SelectList(_context.Entrenadores, "EntrenadorId", "NombreCompleto", vm.EntrenadorId);

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

            ViewData["EntrenadorId"] = new SelectList(_context.Entrenadores, "EntrenadorId", "NombreCompleto", vm.EntrenadorId);
            return View(vm);
        }

        // GET: Clase/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var clase = await _context.Clases.FindAsync(id);
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
                    // Asignar valores predeterminados si no hay un entrenador relacionado
                    EntrenadorId = clase.Entrenadores?.EntrenadorId ?? 0,
                    Nombre = clase.Entrenadores?.Nombre ?? "No asignado",
                    Apellido = clase.Entrenadores?.Apellido ?? "No asignado",
                    Especialidad = clase.Entrenadores?.Especialidad ?? "No asignada" // Asegúrate de tener la propiedad Especialidad en el ViewModel si es necesario
                }
            };

            ViewData["EntrenadorId"] = new SelectList(_context.Entrenadores, "EntrenadorId", "NombreCompleto", vm.EntrenadorId);

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
                    // Asignar valores predeterminados si no hay un entrenador relacionado
                    EntrenadorId = clase.Entrenadores?.EntrenadorId ?? 0,
                    Nombre = clase.Entrenadores?.Nombre ?? "No asignado",
                    Apellido = clase.Entrenadores?.Apellido ?? "No asignado",
                    Especialidad = clase.Entrenadores?.Especialidad ?? "No asignada" // Asegúrate de tener la propiedad Especialidad en el ViewModel si es necesario
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
    }
}
