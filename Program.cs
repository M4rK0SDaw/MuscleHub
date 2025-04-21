using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.AspNetCore.Identity;
using MuscleHub.Data;

var builder = WebApplication.CreateBuilder(args);

// Configurar sesiones
builder.Services.AddSession();
builder.Services.AddDistributedMemoryCache(); // Para usar sesiones en memoria
                                              
// *******************************************

// Configurar el DbContext para MySQL
builder.Services.AddDbContext<GymDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContext");
options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

// *******************************************

// Agregar controladores y vistas
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();

app.UseSession();

app.UseAuthentication(); 

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
