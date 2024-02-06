using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorDeHotel.BS
{
   public interface IServiciosParaHotel
    {
      

        List<Model.InformacionDeHabitacion> ListarHabitaciones();

        void AgregarHabitacion(Model.HabitacionViewModelCreate habitacion);

        Model.InformacionDeHabitacion BuscarHabitacionPorId(int id);

        void Reparar(int id);

        void Habilitar(int id);

        void EditarHabitacion(Model.HabitacionViewModelCreate laHabitacionAEditar);

        Model.InformacionDeHabitacion DetallesDeHabitacion(int id);

        List<GestorDeHotel.Model.TipoHabitacion> ListarTipoHabitaciones();

        List<GestorDeHotel.Model.InformacionReserva> ListarReservaciones();

        List<Model.Reservaciones> ListarTablaReserva();

        List<GestorDeHotel.Model.InformacionDePromociones> ListarPromociones();

        void AgregarTarifa(GestorDeHotel.Model.Tarifa tarifa);

        void EditarTarifa(GestorDeHotel.Model.Tarifa tarifa);

        void AgregarTipoHabitacion(GestorDeHotel.Model.TipoHabitacion tipoHabitacion);

        void EditarTipoHabitacion(GestorDeHotel.Model.TipoHabitacion tipoHabitacion);

        void AgregarPromocion(GestorDeHotel.Model.Promociones promociones);

        void EditarPromocion(GestorDeHotel.Model.Promociones promociones);

        GestorDeHotel.Model.TipoHabitacion ObtenerTipoHabitacionPorId(int id);

        GestorDeHotel.Model.Tarifa ObtenerTarifaPorId(int id);

        GestorDeHotel.Model.Promociones ObtenerPromocionPorId(int id);

        List<GestorDeHotel.Model.Tarifa> ListarTarifas();

        Model.InformacionDePromociones DetallesDePromocion(int id);

        void AgregarReservacion(Model.ReservacionViewModelCreate laReservacion);

        List<Model.InformacionDeHabitacion> ReservarHabitacion(List<Model.InformacionDeHabitacion> listaDeHabitaciones);

        Model.ReservacionViewModelDetalles DetallesReservacion(int idHabitacion);

        Model.ReservacionViewModelDetalles calcularTotalDeReservacion(Model.ReservacionViewModelDetalles reservacionDetalles);

        Model.ReservacionViewModelDetalles CheckOut(int idHabitacion);

        Model.Reservaciones EntregarHabitacion(int idHabitacion);

        Model.ReservacionViewModelDetalles DetallesDeCheckOut(int id);

        int ObtenerReserva();
    }
}
