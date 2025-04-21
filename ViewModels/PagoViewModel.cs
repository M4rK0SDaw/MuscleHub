using System;
using System.ComponentModel.DataAnnotations;

namespace MuscleHub.ViewModels
{
    public class PagoViewModel
    {
        [Key]
        public int PagoId { get; set; }

        [Required(ErrorMessage = "El miembro es obligatorio.")]
        public int MiembroId { get; set; }

        [Required(ErrorMessage = "El monto es obligatorio.")]
        [Range(0.01, 99999.99, ErrorMessage = "El monto debe ser mayor que 0.")]
        public decimal Monto { get; set; }

        [Required(ErrorMessage = "La fecha del pago es obligatoria.")]
        public DateTime Fecha { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "El método de pago es obligatorio.")]
        public int MetodoId { get; set; }

        // Relación con métodos de pago
        public virtual  MetodosPagoViewModel Metodo  { get; set; }

        // Relación con miembros
        public virtual  MiembroViewModel Miembro { get; set; }
    }
}
