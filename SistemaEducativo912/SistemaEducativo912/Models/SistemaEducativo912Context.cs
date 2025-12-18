using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SistemaEducativo912.Models;

public partial class SistemaEducativo912Context : IdentityDbContext
{
    public SistemaEducativo912Context()
    {
    }

    public SistemaEducativo912Context(DbContextOptions<SistemaEducativo912Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Curso> Cursos { get; set; }

    public virtual DbSet<Estudiante> Estudiantes { get; set; }

    public virtual DbSet<Matricula> Matriculas { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=MEDAPRCSGFSP829;Initial Catalog=SistemaEducativo912;integrated security=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Curso>(entity =>
        {
            entity.HasKey(e => e.CursoId).HasName("PK__Curso__7E023A3713F14431");

            entity.ToTable("Curso");

            entity.Property(e => e.CursoId).HasColumnName("CursoID");
            entity.Property(e => e.NombreCurso)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.HasKey(e => e.EstudianteId).HasName("PK__Estudian__6F76833899D75EC8");

            entity.ToTable("Estudiante");

            entity.Property(e => e.EstudianteId).HasColumnName("EstudianteID");
            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Matricula>(entity =>
        {
            entity.HasKey(e => e.MatriculaId).HasName("PK__Matricul__908CEE229B1D2E8E");

            entity.ToTable("Matricula");

            entity.Property(e => e.MatriculaId).HasColumnName("MatriculaID");
            entity.Property(e => e.CursoId).HasColumnName("CursoID");
            entity.Property(e => e.EstudianteId).HasColumnName("EstudianteID");
            entity.Property(e => e.FechaMatricula).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Curso).WithMany(p => p.Matriculas)
                .HasForeignKey(d => d.CursoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Matricula__Curso__3C69FB99");

            entity.HasOne(d => d.Estudiante).WithMany(p => p.Matriculas)
                .HasForeignKey(d => d.EstudianteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Matricula__Estud__3B75D760");
            base.OnModelCreating(modelBuilder);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
