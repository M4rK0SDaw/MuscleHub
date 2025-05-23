using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MuscleHub.Data;
using MuscleHub.Models;
using MuscleHub.ViewModels;

namespace MuscleHub.Controllers
{
    public class PagoController : Controller
    {
        private readonly GymDbContext _context;

        public PagoController(GymDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 10;
            var totalPagos = await _context.Pagos.CountAsync();

            var pago = await _context.Pagos
               .Include(p => p.Metodo) 
               .Include(p => p.Miembro)
               .OrderBy(p => p.PagoId)
               .Skip((page - 1) * pageSize)
               .Take(pageSize)
               .ToListAsync();



            var mv = pago.Select(p => new PagoViewModel
                {
                    PagoId = p.PagoId,
                    MiembroId = p.MiembroId,
                    Monto = p.Monto,
                    Fecha = p.Fecha,
                    MetodoId = p.MetodoId,
                    Metodo = new MetodosPagoViewModel
                    {
                        MetodoId = p.Metodo.MetodoId,
                        Nombre = p.Metodo.Nombre
                    },
                    Miembro = new MiembroViewModel   // inicializador
                    {
                        MiembroId = p.Miembro.MiembroId,
                        Nombre = p.Miembro.Nombre,
                        Apellido = p.Miembro.Apellido,
                        Correo = p.Miembro.Correo,
                        Password = "",
                        Telefono = p.Miembro.Telefono,      
                        Estado = p.Miembro.Estado,
                        FechaRegistro = p.Miembro.FechaRegistro
                    }
                })
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalPagos / pageSize);

            return View(mv);
        }


        // GET: Pago/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var pago = await _context.Pagos
                .Include(p => p.Metodo)
                .Include(p => p.Miembro)
                .FirstOrDefaultAsync(m => m.PagoId == id);

            if (pago == null) return NotFound();

            var viewModel = new PagoViewModel
            {
                PagoId = pago.PagoId,
                MiembroId = pago.MiembroId,
                Monto = pago.Monto,
                Fecha = pago.Fecha,
                MetodoId = pago.MetodoId
            };

            return View(viewModel);
        }

        // GET: Pago/Create
        public IActionResult Create()
        {
            ViewData["MetodoId"] = new SelectList(_context.MetodosPagos, "MetodoId", "Nombre");
            ViewData["MiembroId"] = new SelectList(_context.Miembros, "MiembroId", "Nombre");
            return View();
        }

        // POST: Pago/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PagoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var pago = new PagoModels
                {
                    MiembroId = viewModel.MiembroId,
                    Monto = viewModel.Monto,
                    Fecha = viewModel.Fecha,
                    MetodoId = viewModel.MetodoId
                };

                _context.Add(pago);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["MetodoId"] = new SelectList(_context.MetodosPagos, "MetodoId", "Nombre", viewModel.MetodoId);
            ViewData["MiembroId"] = new SelectList(_context.Miembros, "MiembroId", "Nombre", viewModel.MiembroId);
            return View(viewModel);
        }

        // GET: Pago/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var pago = await _context.Pagos.FindAsync(id);
            if (pago == null) return NotFound();

            var viewModel = new PagoViewModel
            {
                PagoId = pago.PagoId,
                MiembroId = pago.MiembroId,
                Monto = pago.Monto,
                Fecha = pago.Fecha,
                MetodoId = pago.MetodoId
            };

            ViewData["MetodoId"] = new SelectList(_context.MetodosPagos, "MetodoId", "Nombre", pago.MetodoId);
            ViewData["MiembroId"] = new SelectList(_context.Miembros, "MiembroId", "Nombre", pago.MiembroId);
            return View(viewModel);
        }

        // POST: Pago/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PagoViewModel viewModel)
        {
            if (id != viewModel.PagoId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var pago = await _context.Pagos.FindAsync(id);
                    if (pago == null) return NotFound();

                    pago.MiembroId = viewModel.MiembroId;
                    pago.Monto = viewModel.Monto;
                    pago.Fecha = viewModel.Fecha;
                    pago.MetodoId = viewModel.MetodoId;

                    _context.Update(pago);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PagoExists(viewModel.PagoId)) return NotFound();
                    else throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["MetodoId"] = new SelectList(_context.MetodosPagos, "MetodoId", "Nombre", viewModel.MetodoId);
            ViewData["MiembroId"] = new SelectList(_context.Miembros, "MiembroId", "Nombre", viewModel.MiembroId);
            return View(viewModel);
        }

        // GET: Pago/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var pago = await _context.Pagos
                .Include(p => p.Metodo)
                .Include(p => p.Miembro)
                .FirstOrDefaultAsync(m => m.PagoId == id);

            if (pago == null) return NotFound();

            var viewModel = new PagoViewModel
            {
                PagoId = pago.PagoId,
                MiembroId = pago.MiembroId,
                Monto = pago.Monto,
                Fecha = pago.Fecha,
                MetodoId = pago.MetodoId
            };

            return View(viewModel);
        }

        // POST: Pago/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]          
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pago = await _context.Pagos.FindAsync(id);
            if (pago != null)
            {
                _context.Pagos.Remove(pago);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool PagoExists(int id)
        {
            return _context.Pagos.Any(e => e.PagoId == id);
        }
    }
}
