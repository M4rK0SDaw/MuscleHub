using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MuscleHub.Models;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace MuscleHub.Data;

public partial class GymDbContext : DbContext
{
    public GymDbContext()
    {
    }

    public GymDbContext(DbContextOptions<GymDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ClaseModels> Clases { get; set; }

    public virtual DbSet<EntrenadoresModels> Entrenadores { get; set; }

    public virtual DbSet<LoginLogModels> LoginLogs { get; set; }

    public virtual DbSet<MetodosPagoModels> MetodosPagos  { get; set; }

    public virtual DbSet<MiembroModels> Miembros { get; set; }

    public virtual DbSet<PagoModels> Pagos  { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Obtener la cadena de conexión desde el archivo de configuración
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Especifica el directorio de trabajo
                .AddJsonFile("appsettings.json");

            var configuration = builder.Build();
            var connectionString = configuration.GetConnectionString("ApplicationDbContext");

            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<ClaseModels>(entity =>
        {
            entity.HasKey(e => e.ClaseId).HasName("PRIMARY");

            entity.HasIndex(e => e.EntrenadorId, "entrenador_id");

            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.Dia).HasColumnType("enum('lunes','martes','miércoles','jueves','viernes','sábado','domingo')");
            entity.Property(e => e.HoraFin).HasColumnType("time");
            entity.Property(e => e.HoraInicio).HasColumnType("time");
            entity.Property(e => e.Nombre).HasMaxLength(100);

            entity.HasOne(d => d.Entrenadores).WithMany(p => p.Clases)
                .HasForeignKey(d => d.EntrenadorId)
                .HasConstraintName("clases_ibfk_1");
        });

        modelBuilder.Entity<EntrenadoresModels>(entity =>
        {
            entity.HasKey(e => e.EntrenadorId).HasName("PRIMARY");

            entity.HasIndex(e => e.Correo, "correo").IsUnique();

            entity.Property(e => e.Apellido).HasMaxLength(100);
            entity.Property(e => e.Correo).HasMaxLength(100);
            entity.Property(e => e.Especialidad).HasMaxLength(100);
            entity.Property(e => e.Estado).HasDefaultValueSql("'1'");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Telefono).HasMaxLength(20);
        });

        modelBuilder.Entity<LoginLogModels>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PRIMARY");

            entity.HasIndex(e => e.EntrenadorId, "entrenador_id");

            entity.Property(e => e.FechaLogin)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp");
            entity.Property(e => e.Ip).HasMaxLength(45);

            entity.HasOne(d => d.Entrenadores).WithMany(p => p.LoginLogs)
                .HasForeignKey(d => d.EntrenadorId)
                .HasConstraintName("login_logs_ibfk_1");
        });

        modelBuilder.Entity<MetodosPagoModels>(entity =>
        {
            entity.HasKey(e => e.MetodoId).HasName("PRIMARY");

            entity.ToTable("metodos_pago");

            entity.HasIndex(e => e.Nombre, "nombre").IsUnique();

            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<MiembroModels>(entity =>
        {
            entity.HasKey(e => e.MiembroId).HasName("PRIMARY");

            entity.HasIndex(e => e.Correo, "correo").IsUnique();

            entity.Property(e => e.Apellido).HasMaxLength(100);
            entity.Property(e => e.Correo).HasMaxLength(100);
            entity.Property(e => e.Estado).HasDefaultValueSql("'1'");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(20);
        });

        modelBuilder.Entity<PagoModels>(entity =>
        {
            entity.HasKey(e => e.PagoId).HasName("PRIMARY");

            entity.HasIndex(e => e.MetodoId, "metodo_id");

            entity.HasIndex(e => e.MiembroId, "miembro_id");

            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp");
            entity.Property(e => e.Monto).HasPrecision(10, 2);

            entity.HasOne(d => d.Metodo).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.MetodoId)
                .HasConstraintName("pagos_ibfk_2");

            entity.HasOne(d => d.Miembro).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.MiembroId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("pagos_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}