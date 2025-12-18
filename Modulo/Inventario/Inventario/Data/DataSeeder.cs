using Microsoft.AspNetCore.Identity;

namespace Inventario.Data
{
    public static class DataSeeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider services)
        {
            // Obtener servicios desde el scope
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

            // 1. Crear roles si no existen
            string[] roles = new[]
            {
                RolesConstants.Admin,
                RolesConstants.Vendedor
            };

            foreach (var roleName in roles)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // 2. Crear usuario admin por defecto si no existe
            string adminEmail = "admin@inventario.com";
            string adminPassword = "Admin123*"; // cámbiala luego

            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, RolesConstants.Admin);
                }
                else
                {
                    // Puedes loguear errores aquí si quieres
                    throw new Exception("No se pudo crear el usuario Admin inicial.");
                }
            }
            else
            {
                // Asegurar que tenga el rol Admin
                if (!await userManager.IsInRoleAsync(adminUser, RolesConstants.Admin))
                {
                    await userManager.AddToRoleAsync(adminUser, RolesConstants.Admin);
                }
            }
        }
    }
}
