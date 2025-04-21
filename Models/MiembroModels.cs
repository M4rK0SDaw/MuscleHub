using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MuscleHub.Models
{
    public partial class MiembroModels
    {
        public int MiembroId { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Correo { get; set; }

        public string Telefono { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public bool Estado { get; set; }

        public DateTime FechaRegistro { get; set; }

        public virtual ICollection<PagoModels> Pagos { get; set; } = [];
    }
}
