using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MuscleHub.ViewModels
{
    public class MetodosPagoViewModel
    {
        [Key]
        public int MetodoId { get; set; }

        [Required(ErrorMessage = "El nombre del método de pago es obligatorio.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 50 caracteres.")]
        [RegularExpression(@"^[A-Za-z0-9\s]+$", ErrorMessage = "El nombre solo puede contener letras, números y espacios.")]
        public string? Nombre { get; set; }

        // Relación con la clase Pagos
        public virtual ICollection<PagoViewModel> Pagos { get; set; } = new List<PagoViewModel>();
    }
}
