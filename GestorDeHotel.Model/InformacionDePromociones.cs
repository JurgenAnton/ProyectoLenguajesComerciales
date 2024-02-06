using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorDeHotel.Model
{
   public class InformacionDePromociones
    {

        [Display(Name = "ID de la Promoción")]
        public int Id { get; set; }

        [Display(Name = "Tipo de habitación")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public int IdTipoDeHabitacion { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Desde")]
        public DateTime Desde { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Hasta")]
        public DateTime Hasta { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Porcentaje de descuento")]
        public int PorcentajeDeDescuento { get; set; }

        public IEnumerable<TipoHabitacion> ListaDeTiposHabitacion { get; set; }

        [Display(Name = "Tipo de habitación")]
        public string TipoDeHabitacion { get; set; }




    }
}
