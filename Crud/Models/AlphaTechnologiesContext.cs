using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Crud.Models;

public partial class AlphaTechnologiesContext : DbContext
{
    public AlphaTechnologiesContext()
    {
    }

    public AlphaTechnologiesContext(DbContextOptions<AlphaTechnologiesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categorium> Categoria { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__Categori__A3C02A10ABF806FA");

            entity.Property(e => e.NombreCategoria)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Producto__09889210CBC3DFAF");

            entity.ToTable("Producto");

            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Producto__IdCate__145C0A3F");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
