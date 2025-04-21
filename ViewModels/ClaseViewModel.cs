using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MuscleHub.ViewModels
{
    public class ClaseViewModel : IValidatableObject
    {
        [Key]
        public int ClaseId { get; set; }

        [Required(ErrorMessage = "El nombre de la clase es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre de la clase no puede tener más de 100 caracteres.")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "El nombre solo puede contener letras y espacios.")]
        [Display(Name = "Nombre de la Clase")]
        public required string Nombre { get; set; }

        [StringLength(500, ErrorMessage = "La descripción no puede tener más de 500 caracteres.")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[A-Za-z0-9\s.,]+$", ErrorMessage = "La descripción solo puede contener letras, números, espacios y algunos caracteres especiales.")]
        [Display(Name = "Descripción de la Clase")]
        public required string Descripcion { get; set; }

        [Required(ErrorMessage = "El día es obligatorio.")]
        [StringLength(20, ErrorMessage = "El día no puede tener más de 20 caracteres.")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "El día solo puede contener letras y espacios.")]
        [Display(Name = "Día de la Clase")]
        public required string Dia { get; set; }

        [Required(ErrorMessage = "La hora de inicio es obligatoria.")]
        [DataType(DataType.Time)]
        [Display(Name = "Hora de Inicio")]
        public TimeOnly HoraInicio { get; set; }

        [Required(ErrorMessage = "La hora de fin es obligatoria.")]
        [DataType(DataType.Time)]
        [Display(Name = "Hora de Fin")]
        public TimeOnly HoraFin { get; set; }

        [Required(ErrorMessage = "El entrenador es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "Seleccione un entrenador válido.")]
        [Display(Name = "Entrenador")]
        public int EntrenadorId { get; set; }

        public virtual required EntrenadoresViewModel Entrenador { get; set; }

        // Validación personalizada para HoraFin > HoraInicio
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (HoraFin <= HoraInicio)
            {
                yield return new ValidationResult("La hora de fin debe ser posterior a la hora de inicio.",
                    new[] { nameof(HoraFin) });
            }
        }
    }
}
