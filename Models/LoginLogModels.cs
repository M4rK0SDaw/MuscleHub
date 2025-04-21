using System;
using System.Collections.Generic;

namespace MuscleHub.Models
{
    public partial class LoginLogModels
    {
        public int LogId { get; set; }

        public int EntrenadorId { get; set; }

        public DateTime FechaLogin { get; set; }

        public bool Exito { get; set; }

        public string Ip { get; set; }

        // Relación con la clase EntrenadoresModels
        public virtual EntrenadoresModels? Entrenadores { get; set; }
    }
}
