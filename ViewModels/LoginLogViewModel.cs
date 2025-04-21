using System;
using System.ComponentModel.DataAnnotations;

namespace MuscleHub.ViewModels
{
    public class LoginLogViewModel
    {
        [Key]
        public int LogId { get; set; }

        public int? EntrenadorId { get; set; }
       
        public DateTime? FechaLogin { get; set; }
       
        public bool? Exito { get; set; }
       
        public string? Ip { get; set; }

        public virtual EntrenadoresViewModel? Entrenador { get; set; }
    }
}
