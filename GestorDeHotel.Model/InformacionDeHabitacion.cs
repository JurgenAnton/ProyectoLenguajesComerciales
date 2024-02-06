using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GestorDeHotel.Model
{
    public class InformacionDeHabitacion
    {


        public int Id { get; set; }

        public int IdTipoHabitacion { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Número")]
        public int Numero { get; set; } 
      
        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Tipo de habitación")]
        public string TipoHabitacion { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public Estado Estado { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Reservada")]
        public Boolean Reservacion { get; set; }

        public IEnumerable<TipoHabitacion> ListaDeTiposHabitacion { get; set; }







    }
}
