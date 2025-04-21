using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MuscleHub.Models
{
    public partial class MetodosPagoModels
    {
        public int MetodoId { get; set; }

        public required string Nombre { get; set; }

        // Relación con la clase Pagos
        public virtual ICollection<PagoModels> Pagos { get; set; } = [];
    }
}
