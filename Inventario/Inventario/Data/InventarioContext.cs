using System;
using System.Collections.Generic;
using Inventario.Models.Inventario;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Data;

public partial class InventarioContext : DbContext
{
    public InventarioContext(DbContextOptions<InventarioContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AjustesInventario> AjustesInventarios { get; set; }

    public virtual DbSet<AjustesInventarioDetalle> AjustesInventarioDetalles { get; set; }

    public virtual DbSet<Bodega> Bodegas { get; set; }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Compra> Compras { get; set; }

    public virtual DbSet<ComprasDetalle> ComprasDetalles { get; set; }

    public virtual DbSet<MovimientosInventario> MovimientosInventarios { get; set; }

    public virtual DbSet<Proveedore> Proveedores { get; set; }

    public virtual DbSet<Repuesto> Repuestos { get; set; }

    public virtual DbSet<StocksRepuestosBodega> StocksRepuestosBodegas { get; set; }

    public virtual DbSet<Venta> Ventas { get; set; }

    public virtual DbSet<VentasDetalle> VentasDetalles { get; set; }

    // ⬇️ No necesitamos OnConfiguring porque usamos Program.cs
    // Si Scaffold lo vuelve a generar, puedes dejarlo así:
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     if (!optionsBuilder.IsConfigured)
    //     {
    //         optionsBuilder.UseSqlServer("Server=GRYSTO\\SQLEXPRESS;Database=InventarioRepuestosFullDB;Trusted_Connection=True;TrustServerCertificate=True;");
    //     }
    // }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AjustesInventario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AjustesI__3214EC072E887F41");

            entity.Property(e => e.Fecha).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Bodega).WithMany(p => p.AjustesInventarios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ajustes_Bodegas");
        });

        modelBuilder.Entity<AjustesInventarioDetalle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AjustesI__3214EC079B41302B");

            entity.HasOne(d => d.AjusteInventario).WithMany(p => p.AjustesInventarioDetalles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AjustesDetalles_Ajustes");

            entity.HasOne(d => d.Repuesto).WithMany(p => p.AjustesInventarioDetalles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AjustesDetalles_Repuestos");
        });

        modelBuilder.Entity<Bodega>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Bodegas__3214EC0719DE10CA");

            entity.Property(e => e.Activa).HasDefaultValue(true);
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC0775A98C31");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Clientes__3214EC0755E03B55");
        });

        modelBuilder.Entity<Compra>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Compras__3214EC0732843AE9");

            entity.Property(e => e.Fecha).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Bodega).WithMany(p => p.Compras)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Compras_Bodegas");

            entity.HasOne(d => d.Proveedor).WithMany(p => p.Compras)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Compras_Proveedores");
        });

        modelBuilder.Entity<ComprasDetalle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ComprasD__3214EC0787A2F3F5");

            entity.HasOne(d => d.Compra).WithMany(p => p.ComprasDetalles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ComprasDetalles_Compras");

            entity.HasOne(d => d.Repuesto).WithMany(p => p.ComprasDetalles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ComprasDetalles_Repuestos");
        });

        modelBuilder.Entity<MovimientosInventario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Movimien__3214EC07E01D835A");

            entity.HasOne(d => d.Bodega).WithMany(p => p.MovimientosInventarios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Movimientos_Bodegas");

            entity.HasOne(d => d.Repuesto).WithMany(p => p.MovimientosInventarios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Movimientos_Repuestos");
        });

        modelBuilder.Entity<Proveedore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Proveedo__3214EC072A7D902D");
        });

        modelBuilder.Entity<Repuesto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Repuesto__3214EC07A77CE3F9");

            entity.Property(e => e.Activo).HasDefaultValue(true);

            entity.HasOne(d => d.Categoria).WithMany(p => p.Repuestos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Repuestos_Categorias");
        });

        modelBuilder.Entity<StocksRepuestosBodega>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StocksRe__3214EC07C6E34BD8");

            entity.HasOne(d => d.Bodega).WithMany(p => p.StocksRepuestosBodegas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Stocks_Bodegas");

            entity.HasOne(d => d.Repuesto).WithMany(p => p.StocksRepuestosBodegas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Stocks_Repuestos");
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ventas__3214EC071592C417");

            entity.Property(e => e.Fecha).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Bodega).WithMany(p => p.Venta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ventas_Bodegas");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Venta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ventas_Clientes");
        });

        modelBuilder.Entity<VentasDetalle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__VentasDe__3214EC0715550B5D");

            entity.HasOne(d => d.Repuesto).WithMany(p => p.VentasDetalles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VentasDetalles_Repuestos");

            entity.HasOne(d => d.Venta).WithMany(p => p.VentasDetalles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VentasDetalles_Ventas");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
