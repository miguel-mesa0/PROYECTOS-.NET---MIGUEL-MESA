using Inventario.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Cadena de conexión desde appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// 2. DbContext de Identity
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// 3. DbContext del Inventario (generado por Scaffold-DbContext)
builder.Services.AddDbContext<InventarioContext>(options =>
    options.UseSqlServer(connectionString));

// 4. Identity + Roles
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    // Aquí puedes ajustar reglas de contraseña si quieres
    // options.Password.RequireNonAlphanumeric = false;
    // options.Password.RequiredLength = 6;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// 5. MVC + Razor Pages (Identity usa Razor Pages)
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// 6. Pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 7. Autenticación y autorización
app.UseAuthentication();
app.UseAuthorization();

// 8. Static assets (nuevo template .NET 8)
app.MapStaticAssets();

// 9. Rutas MVC
app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// 10. Rutas de Razor Pages (Login, Register, etc.)
app.MapRazorPages();

// 11. Seed de roles y usuario Admin inicial
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await DataSeeder.SeedRolesAndAdminAsync(services);
}

// 12. Ejecutar la app
await app.RunAsync();
