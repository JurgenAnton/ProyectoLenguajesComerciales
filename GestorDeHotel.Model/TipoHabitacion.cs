using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GestorDeHotel.Model
{
    public class TipoHabitacion
    {

        [Display(Name = "ID de la habitación")]
        public int Id { get; set; }


        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

    }
}
