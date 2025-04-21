using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MuscleHub.Models
{
    public partial class MiembroModels
    {
        public int MiembroId { get; set; }

        public required string Nombre { get; set; }

        public required string Apellido { get; set; } 

        public required  string Correo { get; set; } 

        public required string Password { get; set; } = null!;

        public required  string Telefono { get; set; }="";

        public DateOnly FechaNacimiento { get; set; }

        public bool Estado { get; set; }

        public DateTime FechaRegistro { get; set; }

        public virtual ICollection<PagoModels> Pagos { get; set; } = [];
    }
}