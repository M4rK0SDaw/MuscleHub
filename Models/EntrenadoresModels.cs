using System;
using System.Collections.Generic;

namespace MuscleHub.Models
{
    public partial class EntrenadoresModels
    {
        public int EntrenadorId { get; set; }

        public required string Nombre { get; set; }

        public required string Apellido { get; set; }

        public required string Correo { get; set; }

        public required string Telefono { get; set; }

        public  string Especialidad { get; set; } = "";

        public required string Password { get; set; } = null!;

        public bool Estado { get; set; } = true;

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        // Relación con las clases
        public virtual ICollection<ClaseModels> Clases { get; set; } = new List<ClaseModels>();

      // Relación con los logs de login
        public virtual ICollection<LoginLogModels> LoginLogs { get; set; } =  new List<LoginLogModels>();
    }
}
