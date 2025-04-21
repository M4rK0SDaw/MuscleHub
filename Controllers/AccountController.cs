using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MuscleHub.Data;
using MuscleHub.ViewModels;

public class AccountController : Controller
{
    private readonly GymDbContext _context;

    public AccountController(GymDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        var entrenador = await _context.Entrenadores.FirstOrDefaultAsync(e => e.Correo == vm.Correo && e.Password == vm.Password && e.Estado);
        var miembro = await _context.Miembros.FirstOrDefaultAsync(m => m.Correo == vm.Correo && m.Password == vm.Password && m.Estado);

        if (entrenador is null)
        {
            // Login fallido
            _ = _context.LoginLogs.Add(new MuscleHub.Models.LoginLogModels
            {
                EntrenadorId = 0, // o null si permites
                Exito = false,
                Ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "IP desconocida", // Manejo de posible null
                FechaLogin = DateTime.Now
            });
            await _context.SaveChangesAsync();

            vm.ErrorMessage = "Correo o contraseña incorrectos";
            return View(vm);
        }

        // Registrar en loginlogs
        _context.LoginLogs.Add(new MuscleHub.Models.LoginLogModels
        {
            EntrenadorId = entrenador.EntrenadorId,
            Exito = true,
            Ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "IP desconocida", // Manejo de posible null
            FechaLogin = DateTime.Now
        });
        await _context.SaveChangesAsync();

        // Guardar en sesión
        HttpContext.Session.SetInt32("EntrenadorId", entrenador.EntrenadorId);
        HttpContext.Session.SetString("EntrenadorNombre", entrenador.Nombre);

        return RedirectToAction("Index", "Home");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}
