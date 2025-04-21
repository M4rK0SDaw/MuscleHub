using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MuscleHub.Data;
using MuscleHub.Models;
using MuscleHub.ViewModels;

namespace MuscleHub.Controllers
{
    public class MetodosPagoController : Controller
    {
        private readonly GymDbContext _context;

        public MetodosPagoController(GymDbContext context)
        {
            _context = context;
        }

        // GET: MetodosPago
        public async Task<IActionResult> Index()
        {
            var metodos = await _context.MetodosPagos
                .Select(m => new MetodosPagoViewModel
                {
                    MetodoId = m.MetodoId,
                    Nombre = m.Nombre,
                    Pagos = m.Pagos.Select(p => new PagoViewModel
                    {
                        PagoId = p.PagoId,
                        Monto = p.Monto,
                        Fecha = p.Fecha
                    }).ToList()
                })
                .ToListAsync();
            return View(metodos);
        }

        // GET: MetodosPago/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var metodo = await _context.MetodosPagos
                .Include(m => m.Pagos)
                .FirstOrDefaultAsync(m => m.MetodoId == id);

            if (metodo == null) return NotFound();

            var viewModel = new MetodosPagoViewModel
            {
                MetodoId = metodo.MetodoId,
                Nombre = metodo.Nombre,
                Pagos = metodo.Pagos.Select(p => new PagoViewModel
                {
                    PagoId = p.PagoId,
                    Monto = p.Monto,
                    Fecha = p.Fecha
                }).ToList()
            };

            return View(viewModel);
        }

        // GET: MetodosPago/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MetodosPago/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MetodosPagoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var metodo = new MetodosPagoModels
                {
                    Nombre = viewModel.Nombre
                };

                _context.Add(metodo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        // GET: MetodosPago/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var metodo = await _context.MetodosPagos.FindAsync(id);
            if (metodo == null) return NotFound();

            var viewModel = new MetodosPagoViewModel
            {
                MetodoId = metodo.MetodoId,
                Nombre = metodo.Nombre
            };

            return View(viewModel);
        }

        // POST: MetodosPago/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MetodosPagoViewModel viewModel)
        {
            if (id != viewModel.MetodoId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var metodo = await _context.MetodosPagos.FindAsync(id);
                    if (metodo == null) return NotFound();

                    metodo.Nombre = viewModel.Nombre;

                    _context.Update(metodo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MetodoExists(viewModel.MetodoId)) return NotFound();
                    else throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        // GET: MetodosPago/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var metodo = await _context.MetodosPagos.FindAsync(id);
            if (metodo == null) return NotFound();

            var viewModel = new MetodosPagoViewModel
            {
                MetodoId = metodo.MetodoId,
                Nombre = metodo.Nombre
            };

            return View(viewModel);
        }

        // POST: MetodosPago/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var metodo = await _context.MetodosPagos.FindAsync(id);
            if (metodo != null)
            {
                _context.MetodosPagos.Remove(metodo);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool MetodoExists(int id)
        {
            return _context.MetodosPagos.Any(e => e.MetodoId == id);
        }
    }
}
