using System;
using System.ComponentModel.DataAnnotations;

namespace MuscleHub.ViewModels
{
    public class PagoViewModel
    {
        [Key]
        public int PagoId { get; set; }

        [Required(ErrorMessage = "El miembro es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "Seleccione un miembro válido.")]
        [Display(Name = "Miembro")]
        public int MiembroId { get; set; }

        [Required(ErrorMessage = "El monto es obligatorio.")]
        [Range(0.01, 99999.99, ErrorMessage = "El monto debe ser mayor que 0.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Monto")]
        public decimal Monto { get; set; }

        [Required(ErrorMessage = "La fecha del pago es obligatoria.")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Fecha del Pago")]
        public DateTime Fecha { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "El método de pago es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "Seleccione un método de pago válido.")]
        [Display(Name = "Método de Pago")]
        public int MetodoId { get; set; }

        // Relaciones (opcionales si no estás cargándolas en la vista)
        public virtual MetodosPagoViewModel? Metodo { get; set; }
        public virtual MiembroViewModel? Miembro { get; set; }
    }
}
