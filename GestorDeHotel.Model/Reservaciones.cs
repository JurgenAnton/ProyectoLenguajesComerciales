using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GestorDeHotel.Model
{
    public class Reservaciones
    {

        [Display(Name = "ID de la Reserva")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo ID Habitacion es requerido")]
        [Display(Name = "Id de la habitación")]
        public int Id_Habitacion { get; set; }

        [Required(ErrorMessage = "El campo Horas es requerido")]
        [Display(Name = "Horas Ocupada")]
        public int CantidadDeHoras { get; set; }

        [Display(Name = "Fecha de Salida")]
        public DateTime FechaDeEntrada { get; set; }

        [Display(Name = "Fecha de Salida")]
        public DateTime FechaDeSalida { get; set; }

        public EstadoDeReservacion Estado { get; set; }

    }
}
