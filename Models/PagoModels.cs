using System;
using System.Collections.Generic;

namespace MuscleHub.Models
{
    public partial class PagoModels
    {
        public int PagoId { get; set; }

        public int MiembroId { get; set; }

        public decimal Monto { get; set; }

        public DateTime Fecha { get; set; }

        public int MetodoId { get; set; }

        public virtual MetodosPagoModels? Metodo { get; set; }

        public virtual MiembroModels? Miembro { get; set; }
    }
}
