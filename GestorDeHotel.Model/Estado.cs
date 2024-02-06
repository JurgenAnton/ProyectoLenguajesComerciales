using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;



namespace GestorDeHotel.Model
{
    public enum Estado
    {
        [Display(Name = "Buenas condiciones")]
        BuenasCondiciones = 1,
        [Display(Name = "En reparación")]
        EnReparacion = 2
 

    }
}
