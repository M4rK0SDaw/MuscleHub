﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MuscleHub.Data;

#nullable disable

namespace MuscleHub.Migrations
{
    [DbContext(typeof(GymDbContext))]
    [Migration("20250420120302_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("utf8mb4_0900_ai_ci")
                .HasAnnotation("ProductVersion", "8.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.HasCharSet(modelBuilder, "utf8mb4");
            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("MuscleHub.Models.ClaseModels", b =>
                {
                    b.Property<int>("ClaseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("ClaseId"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Dia")
                        .IsRequired()
                        .HasColumnType("enum('lunes','martes','miércoles','jueves','viernes','sábado','domingo')");

                    b.Property<int>("EntrenadorId")
                        .HasColumnType("int");

                    b.Property<TimeOnly>("HoraFin")
                        .HasColumnType("time");

                    b.Property<TimeOnly>("HoraInicio")
                        .HasColumnType("time");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("ClaseId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "EntrenadorId" }, "entrenador_id");

                    b.ToTable("Clases");
                });

            modelBuilder.Entity("MuscleHub.Models.EntrenadoresModels", b =>
                {
                    b.Property<int>("EntrenadorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("EntrenadorId"));

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Especialidad")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("Estado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValueSql("'1'");

                    b.Property<DateTime?>("FechaRegistro")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.HasKey("EntrenadorId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "Correo" }, "correo")
                        .IsUnique();

                    b.ToTable("Entrenadores");
                });

            modelBuilder.Entity("MuscleHub.Models.LoginLogModels", b =>
                {
                    b.Property<int>("LogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("LogId"));

                    b.Property<int>("EntrenadorId")
                        .HasColumnType("int");

                    b.Property<bool>("Exito")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("FechaLogin")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Ip")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)");

                    b.HasKey("LogId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "EntrenadorId" }, "entrenador_id")
                        .HasDatabaseName("entrenador_id1");

                    b.ToTable("LoginLogs");
                });

            modelBuilder.Entity("MuscleHub.Models.MetodosPagoModels", b =>
                {
                    b.Property<int>("MetodoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("MetodoId"));

                    b.Property<string>("Nombre")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("MetodoId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "Nombre" }, "nombre")
                        .IsUnique();

                    b.ToTable("metodos_pago", (string)null);
                });

            modelBuilder.Entity("MuscleHub.Models.MiembroModels", b =>
                {
                    b.Property<int>("MiembroId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("MiembroId"));

                    b.Property<string>("Apellido")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Correo")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<bool?>("Estado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValueSql("'1'");

                    b.Property<DateOnly?>("FechaNacimiento")
                        .HasColumnType("date");

                    b.Property<DateTime?>("FechaRegistro")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Nombre")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Telefono")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.HasKey("MiembroId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "Correo" }, "correo")
                        .IsUnique()
                        .HasDatabaseName("correo1");

                    b.ToTable("Miembros");
                });

            modelBuilder.Entity("MuscleHub.Models.PagoModels", b =>
                {
                    b.Property<int>("PagoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("PagoId"));

                    b.Property<DateTime>("Fecha")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<int>("MetodoId")
                        .HasColumnType("int");

                    b.Property<int>("MiembroId")
                        .HasColumnType("int");

                    b.Property<decimal>("Monto")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("PagoId")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "MetodoId" }, "metodo_id");

                    b.HasIndex(new[] { "MiembroId" }, "miembro_id");

                    b.ToTable("Pagos");
                });

            modelBuilder.Entity("MuscleHub.Models.ClaseModels", b =>
                {
                    b.HasOne("MuscleHub.Models.EntrenadoresModels", "Entrenadores")
                        .WithMany("Clases")
                        .HasForeignKey("EntrenadorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("clases_ibfk_1");

                    b.Navigation("Entrenadores");
                });

            modelBuilder.Entity("MuscleHub.Models.LoginLogModels", b =>
                {
                    b.HasOne("MuscleHub.Models.EntrenadoresModels", "Entrenadores")
                        .WithMany("LoginLogs")
                        .HasForeignKey("EntrenadorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("login_logs_ibfk_1");

                    b.Navigation("Entrenadores");
                });

            modelBuilder.Entity("MuscleHub.Models.PagoModels", b =>
                {
                    b.HasOne("MuscleHub.Models.MetodosPagoModels", "Metodo")
                        .WithMany("Pagos")
                        .HasForeignKey("MetodoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("pagos_ibfk_2");

                    b.HasOne("MuscleHub.Models.MiembroModels", "Miembro")
                        .WithMany("Pagos")
                        .HasForeignKey("MiembroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("pagos_ibfk_1");

                    b.Navigation("Metodo");

                    b.Navigation("Miembro");
                });

            modelBuilder.Entity("MuscleHub.Models.EntrenadoresModels", b =>
                {
                    b.Navigation("Clases");

                    b.Navigation("LoginLogs");
                });

            modelBuilder.Entity("MuscleHub.Models.MetodosPagoModels", b =>
                {
                    b.Navigation("Pagos");
                });

            modelBuilder.Entity("MuscleHub.Models.MiembroModels", b =>
                {
                    b.Navigation("Pagos");
                });
#pragma warning restore 612, 618
        }
    }
}
