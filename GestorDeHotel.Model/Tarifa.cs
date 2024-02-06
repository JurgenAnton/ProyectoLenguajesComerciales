using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorDeHotel.Model
{
    public class Tarifa
    {
        [Display(Name = "ID de la Tarifa")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Desde")]
        public DateTime Desde { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Hasta")]
        public DateTime Hasta { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Monto por hora")]
        public decimal MontoXHora { get; set; }

    }
}
