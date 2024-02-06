using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorDeHotel.Model
{
    public enum  EstadoDeReservacion
    {
        [Display(Name = "En proceso")]
        EnProceso = 1,
      
        Entregada = 2,
       


    }
}
