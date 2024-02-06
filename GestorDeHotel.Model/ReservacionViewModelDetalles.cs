using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorDeHotel.Model
{
    public class ReservacionViewModelDetalles
    {
        [Display(Name = "Fecha de entrada")]
        public DateTime FechaDeEntrada { get; set; }

        [Display(Name = "Fecha de salida")]
        public DateTime FechaDeSalida { get; set; }

        [Display(Name = "Monto por hora")]
        public decimal montoPorHora { get; set; }

        [Display(Name = "Cantidad de horas")]
        public int CantidadDeHoras { get; set; }

        [Display(Name = "Monto de descuento")]
        public int montoDescuento { get; set; }

        [Display(Name = "Monto total de la reserva")]
        public int montoTotalDeLaReserva { get; set; }

        public int porcentajeDescuento { get; set; } 
        
        public int IdTipoDeHabitacion { get; set; }


    }
}
