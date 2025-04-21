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
        [StringLength(100)]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "El nombre solo puede contener letras y espacios.")]
        [Display(Name = "Nombre")]
        public required string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(100)]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "El apellido solo puede contener letras y espacios.")]
        [Display(Name = "Apellido")]
        public string Apellido { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "Correo electrónico no válido.")]
        [StringLength(100)]
        [Display(Name = "Correo Electrónico")]
        public required string Correo { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(100, MinimumLength = 6)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]).{6,}$",
            ErrorMessage = "Debe tener una mayúscula, minúscula, número, carácter especial y en total 6 carácteres")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public required string Password { get; set; }     

        [Required(ErrorMessage = "El número de teléfono es obligatorio.")]
        [Phone(ErrorMessage = "Número de teléfono no válido.")]
        [StringLength(15)]
        [RegularExpression(@"^\+?[0-9\s]+$", ErrorMessage = "Solo números y espacios.")]
        [Display(Name = "Teléfono")]
        public required string Telefono { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Nacimiento")]               
        public DateOnly FechaNacimiento { get; set; }

        public bool Estado { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime FechaRegistro { get; set; }

        public virtual ICollection<PagoViewModel> Pagos { get; set; } = new List<PagoViewModel>();       

    }
}
