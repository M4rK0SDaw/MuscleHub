using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.Web.CodeGeneration.Utils;

namespace MuscleHub.ViewModels
{
    public class EntrenadoresViewModel
    {
        [Key]
        public int EntrenadorId { get; set; }

        [Required(ErrorMessage = "El nombre del entrenador es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "El nombre solo puede contener letras y espacios.")]
        [Display(Name = "Nombre del Entrenador")]
        public string Nombre { get; set; } 

        [Required(ErrorMessage = "El apellido del entrenador es obligatorio.")]
        [StringLength(100, ErrorMessage = "El apellido no puede superar los 100 caracteres.")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "El apellido solo puede contener letras y espacios.")]
        [Display(Name = "Apellido del Entrenador")]
        public string Apellido { get; set; } 
        [Required(ErrorMessage = "El correo del entrenador es obligatorio.")]
        [EmailAddress(ErrorMessage = "Correo electrónico no válido.")]
        [StringLength(100, ErrorMessage = "El correo electrónico no puede superar los 100 caracteres.")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Correo Electrónico")]
        public string Correo { get; set; } 

        [Required(ErrorMessage = "El teléfono del entrenador es obligatorio.")]
        [Phone(ErrorMessage = "Número de teléfono no válido.")]
        [StringLength(15, ErrorMessage = "El número de teléfono no puede superar los 15 caracteres.")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Teléfono")]
        [RegularExpression(@"^\+?[0-9\s]+$", ErrorMessage = "El teléfono solo puede contener números y espacios.")]
        public string Telefono { get; set; } 

        [Required(ErrorMessage = "La especialidad del entrenador es obligatoria.")]
        [StringLength(200, ErrorMessage = "La especialidad no puede superar los 200 caracteres.")]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "La especialidad solo puede contener letras y espacios.")]
        [Display(Name = "Especialidad")]
        public string Especialidad { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]).{6,}$",
            ErrorMessage = "La contraseña debe contener al menos una mayúscula, una minúscula, un número y un carácter especial.")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }    

        public bool Estado { get; set; } = true;

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public virtual ICollection<ClaseViewModel> Clases { get; set; } = new List<ClaseViewModel>();
        public virtual ICollection<LoginLogViewModel> LoginLogs { get; set; } = new List<LoginLogViewModel>();
    }
}
