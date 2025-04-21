using System;
using System.Collections.Generic;

namespace MuscleHub.Models
{
    public partial class EntrenadoresModels
    {
        public int EntrenadorId { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Correo { get; set; }

        public string Telefono { get; set; }

        public string Especialidad { get; set; } = "";

        public string Password { get; set; } = null!;

        public bool Estado { get; set; } = true;

        public DateTime? FechaRegistro { get; set; }

        // Relación con las clases
        public virtual ICollection<ClaseModels> Clases { get; set; } = [];

        // Relación con los logs de login
        public virtual ICollection<LoginLogModels> LoginLogs { get; set; } = [];
    }
}
