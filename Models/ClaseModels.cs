using System;
using System.Collections.Generic;

namespace MuscleHub.Models
{
    public partial class ClaseModels
    {
        public int ClaseId { get; set; }

        public required string Nombre { get; set; }

        public required string Descripcion { get; set; }

        public required string Dia { get; set; }

        public TimeOnly HoraInicio { get; set; }

        public TimeOnly HoraFin { get; set; }

        public int EntrenadorId { get; set; }

        // Relación con los entrenadores
        public virtual EntrenadoresModels? Entrenadores { get; set; }       
    }
}
