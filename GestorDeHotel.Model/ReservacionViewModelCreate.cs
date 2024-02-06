using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorDeHotel.Model
{
   public class ReservacionViewModelCreate
    {

        [Display(Name = "ID de la Reserva")]
        public int Id { get; set; }

        [Display(Name = "ID de la Habitación")]
        public int Id_Habitacion { get; set; }

        [Required(ErrorMessage = "El campo Estado es requerido")]
        public Estado Estado { get; set; }

        [Required(ErrorMessage = "El campo Horas es requerido")]
        [Display(Name = "Horas Ocupada")]
        public int CantidadDeHoras { get; set; }

        [Display(Name = "Fecha de Salida")]
        public DateTime FechaDeSalida { get; set; }

        [Display(Name = "Fecha de Entrada")]
        public DateTime FechaDeEntrada { get; set; }

        public Boolean Reservacion { get; set; }

        [Required(ErrorMessage = "El campo Estado es requerido")]
        public EstadoDeReservacion EstadoReservacion { get; set; }





    }
}
