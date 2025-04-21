using System;
using System.ComponentModel.DataAnnotations;

namespace MuscleHub.ViewModels
{
    public class ClaseViewModel
    {
        [Key]
        public int ClaseId { get; set; }

        [Required(ErrorMessage = "El nombre de la clase es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre de la clase no puede tener más de 100 caracteres.")]
        public required string Nombre { get; set; }

        [StringLength(500, ErrorMessage = "La descripción no puede tener más de 500 caracteres.")]
        public required string Descripcion { get; set; }

        [Required(ErrorMessage = "El día es obligatorio.")]
        public required string Dia { get; set; }

        [Required(ErrorMessage = "La hora de inicio es obligatoria.")]
        public TimeSpan HoraInicio { get; set; }

        [Required(ErrorMessage = "La hora de fin es obligatoria.")]
        public TimeSpan HoraFin { get; set; }

        [Required(ErrorMessage = "El ID del entrenador es obligatorio.")]
        public int EntrenadorId { get; set; }

        // Relación con el entrenador
        public virtual required EntrenadoresViewModel Entrenador { get; set; }
    }
}
