using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorDeHotel.Model
{
    public class HabitacionViewModelCreate
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El Número es requerido")]
        [Display(Name = "Número")]
        public int Numero { get; set; } 

        public int IdTipoHabitacion { get; set; }





    }
}
