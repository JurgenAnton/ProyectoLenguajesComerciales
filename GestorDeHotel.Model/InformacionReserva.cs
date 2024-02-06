using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GestorDeHotel.Model
{
    public class InformacionReserva
    {
        [Display(Name = "ID de la Reserva")]
        public int Id { get; set; }

        [Display(Name = "ID de la Habitación")]
        public int Id_Habitacion { get; set; }

        [Required(ErrorMessage = "El Número es requerido")]
        [Display(Name = "Número")]
        public int Numero { get; set; }

        [Required(ErrorMessage = "El campo Tipo de habitación es requerido")]
        [Display(Name = "Tipo de habitación")]
        public string TipoHabitacion { get; set; }

        [Required(ErrorMessage = "El campo Estado es requerido")]
        public Estado Estado { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Cantidad de horas ocupada")]
        public int CantidadDeHoras { get; set; }

        [Display(Name = "Fecha de salida")]
        public DateTime FechaDeSalida { get; set; }

        [Display(Name = "Fecha de entrada")]
        public DateTime FechaDeEntrada { get; set; }

        [Display(Name = "Reservada")]
        public Boolean Reservacion { get; set; }

        [Required(ErrorMessage = "El campo Estado es requerido")]
        public EstadoDeReservacion EstadoReservacion { get; set; }

    }
}
