using CrudNativo.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudNativo.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Producto> Producto { get; set; }

    }
}
