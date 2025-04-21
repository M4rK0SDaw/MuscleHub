using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MuscleHub.ViewModels
{
    public class EntrenadoresViewModel
    {
        [Key]
        public int EntrenadorId { get; set; }

        [Required(ErrorMessage = "El nombre del entrenador es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "El apellido del entrenador es obligatorio.")]
        [StringLength(100, ErrorMessage = "El apellido no puede superar los 100 caracteres.")]
        public string? Apellido { get; set; }

        [Required(ErrorMessage = "El correo del entrenador es obligatorio.")]
        [EmailAddress(ErrorMessage = "Correo electrónico no válido.")]
        public string? Correo { get; set; }

        [Required(ErrorMessage = "El teléfono del entrenador es obligatorio.")]
        [Phone(ErrorMessage = "Número de teléfono no válido.")]
        public string? Telefono { get; set; }

        [StringLength(200, ErrorMessage = "La especialidad no puede superar los 200 caracteres.")]
        public string? Especialidad { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        public string Password { get; set; } = null!;

        public bool Estado { get; set; }

        public DateTime? FechaRegistro { get; set; }

        // Relación con las clases
        public virtual ICollection<ClaseViewModel> Clases { get; set; } = new List<ClaseViewModel>();

        // Relación con los logs de login
        public virtual ICollection<LoginLogViewModel> LoginLogs { get; set; } = new List<LoginLogViewModel>();
    }
}
