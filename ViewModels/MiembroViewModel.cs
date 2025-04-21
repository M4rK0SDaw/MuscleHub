using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MuscleHub.ViewModels
{
    public class MiembroViewModel
    {
        [Key]
        public int MiembroId { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
        public required string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(100, ErrorMessage = "El apellido no puede tener más de 100 caracteres.")]
        public string? Apellido { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "Correo electrónico no válido.")]
        public string? Correo { get; set; }

        [Required(ErrorMessage = "El número de teléfono es obligatorio.")]
        [Phone(ErrorMessage = "Número de teléfono no válido.")]
        public string? Telefono { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        public DateTime FechaNacimiento { get; set; }

        public bool Estado { get; set; }

        public DateTime FechaRegistro { get; set; }

        // Relación con pagos
        public virtual ICollection<PagoViewModel> Pagos { get; set; } = new List<PagoViewModel>();
    }
}
