using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorDeHotel.Model
{
    public class Habitacion
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "El Número es requerido")]
        [Display(Name = "Número")]
        public int Numero { get; set; }

        [Required(ErrorMessage = "El campo Tipo de habitación es requerido")]
        [Display(Name = "Tipo de habitación")]
        public int IdTipoHabitacion { get; set; }

        [Required(ErrorMessage = "El campo Estado es requerido")]
        public Estado Estado { get; set; }


    }
}
