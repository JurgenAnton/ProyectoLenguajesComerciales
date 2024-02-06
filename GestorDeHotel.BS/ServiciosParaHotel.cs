using GestorDeHotel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorDeHotel.BS
{
   public class ServiciosParaHotel: IServiciosParaHotel
    {

        private DA.DbContexto ContextoBD;

        public ServiciosParaHotel(DA.DbContexto contextoBD)
        {

           ContextoBD = contextoBD;

        }

        public void AgregarTarifa(GestorDeHotel.Model.Tarifa tarifa)
        {
            ContextoBD.Tarifa.Add(tarifa);
            ContextoBD.SaveChanges();
        }  

        public void EditarTarifa(GestorDeHotel.Model.Tarifa tarifa)
        {

            GestorDeHotel.Model.Tarifa laTarifaAModificar;
            laTarifaAModificar = ObtenerTarifaPorId(tarifa.Id);

            laTarifaAModificar.Desde = tarifa.Desde;
            laTarifaAModificar.Hasta = tarifa.Hasta;
            laTarifaAModificar.MontoXHora = tarifa.MontoXHora;

            ContextoBD.Entry(laTarifaAModificar).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            ContextoBD.Tarifa.Update(laTarifaAModificar);
            ContextoBD.SaveChanges();
   
        }

        public List<Tarifa> ListarTarifas()
        {
            var resultadoTarifas = from c in ContextoBD.Tarifa
                                   select c;

            return resultadoTarifas.ToList();
        }

        public Tarifa ObtenerTarifaPorId(int Id)
        {
            GestorDeHotel.Model.Tarifa resultado;

            resultado = ContextoBD.Tarifa.Find(Id);

            return resultado;
        }   

        public void AgregarHabitacion(HabitacionViewModelCreate habitacion)
        {
            Habitacion laHabitacion = new Habitacion();

            laHabitacion.Estado = Estado.BuenasCondiciones;
            laHabitacion.Numero = habitacion.Numero;
            laHabitacion.IdTipoHabitacion = habitacion.IdTipoHabitacion;
            ContextoBD.Add(laHabitacion);
            ContextoBD.SaveChanges();       
         
        }    

        public void Reparar(int id)
        {

            Model.Habitacion laHabitacion;

            laHabitacion = ContextoBD.Habitacion.Find(id);

            laHabitacion.Estado = Estado.EnReparacion;

            ContextoBD.Habitacion.Update(laHabitacion);
            ContextoBD.SaveChanges();

        }

        public void Habilitar(int id)
        {
            Model.Habitacion laHabitacion;

            laHabitacion = ContextoBD.Habitacion.Find(id);

            laHabitacion.Estado = Estado.BuenasCondiciones;

            ContextoBD.Habitacion.Update(laHabitacion);
            ContextoBD.SaveChanges();


        }

        public InformacionDeHabitacion BuscarHabitacionPorId(int id)
        {
            Model.InformacionDeHabitacion resultado = new Model.InformacionDeHabitacion();
            Habitacion HabitacionAModificar;

            HabitacionAModificar = ContextoBD.Habitacion.Find(id);

            resultado.Numero = HabitacionAModificar.Numero;
            resultado.IdTipoHabitacion = HabitacionAModificar.IdTipoHabitacion;
            resultado.Id = HabitacionAModificar.Id;
           
            
            return resultado;

        }

        public void EditarHabitacion(Model.HabitacionViewModelCreate HabitacionEditada)
        {
            Model.Habitacion HabitacionModificada = new Model.Habitacion();

            HabitacionModificada = ContextoBD.Habitacion.Find(HabitacionEditada.Id);

            HabitacionModificada.Numero = HabitacionEditada.Numero;
            HabitacionModificada.IdTipoHabitacion = HabitacionEditada.IdTipoHabitacion;

            ContextoBD.Habitacion.Update(HabitacionModificada);
            ContextoBD.SaveChanges();

        }

        public List<InformacionDeHabitacion> ListarHabitaciones()
        {
           

            var resultado = from c in ContextoBD.Habitacion
                            join d in ContextoBD.TipoHabitacion on c.IdTipoHabitacion equals d.Id
                            select new InformacionDeHabitacion
                            {
                                Id = c.Id,
                                Numero = c.Numero,
                                Estado = c.Estado,
                                TipoHabitacion = d.Nombre,  
                               
                                
                            };



            return resultado.ToList();

          
              

        }

        public List<InformacionReserva> ListarReservaciones()
        {
           
            List<InformacionReserva> laListaDeReservaciones = new List<InformacionReserva> ();

            List<InformacionDeHabitacion> laListaDeHabitaciones;

            laListaDeHabitaciones = ListarHabitaciones();
           
            List<InformacionReserva> laListaDeReservas;

            laListaDeReservas = ListarReservas(); 

           

            foreach (var itemHabitacion in laListaDeHabitaciones)

                {
               
                    Model.InformacionReserva laReserva = new Model.InformacionReserva ();

                    laReserva.Numero = itemHabitacion.Numero;
                    laReserva.TipoHabitacion = itemHabitacion.TipoHabitacion;
                    laReserva.Estado = itemHabitacion.Estado;
                    laReserva.Id_Habitacion = itemHabitacion.Id;
                   

                foreach (var item in laListaDeReservas)
                {
                    
                    if (item.Id_Habitacion == itemHabitacion.Id)
                    {
                        laReserva.Id = item.Id;
                        laReserva.CantidadDeHoras = item.CantidadDeHoras;
                        laReserva.FechaDeSalida = item.FechaDeSalida;
                        laReserva.EstadoReservacion = item.EstadoReservacion;


                        if (item.EstadoReservacion == EstadoDeReservacion.EnProceso)
                        {

                            laReserva.Reservacion = true;

                        }
                        else
                        {
                            laReserva.Reservacion = false;
                        }

                        laListaDeReservaciones.Add(laReserva);
                    }

                    laListaDeReservaciones.Add(laReserva);

                }

                 laListaDeReservaciones.Add(laReserva);


            }

            return laListaDeReservaciones.Distinct().ToList();


        }

        public List<InformacionReserva> ListarReservas()
        {

            var resultado = from c in ContextoBD.Reservaciones
                            join d in ContextoBD.Habitacion on c.Id_Habitacion equals d.Id
                            select new InformacionReserva
                            {
                                Id = c.Id,
                                Id_Habitacion = d.Id,
                                CantidadDeHoras = c.CantidadDeHoras,
                                FechaDeSalida = c.FechaDeSalida,
                                EstadoReservacion = c.Estado
                                        

                            };


            return resultado.ToList();

        }

        public List<Reservaciones> ListarTablaReserva()
        {

            var resultadoReservaciones = from c in ContextoBD.Reservaciones
                                            select c;

            return resultadoReservaciones.ToList();

        }

        public InformacionDeHabitacion DetallesDeHabitacion(int id)
        {
            InformacionDeHabitacion DetallesDeLaHabitacion;

            List<InformacionDeHabitacion> laListaDeHabitaciones = ListarHabitaciones();

            List<InformacionDeHabitacion> laListaDeHabitacionesActualizada = ReservarHabitacion(laListaDeHabitaciones);

            foreach (var item in laListaDeHabitacionesActualizada)
            {
                if (item.Id == id)
                {

                   DetallesDeLaHabitacion = item;

                   return DetallesDeLaHabitacion;

                }

            }

            return null;
           

        }

        public List<TipoHabitacion> ListarTipoHabitaciones()
        {
            var resultadoTipoHabitaciones = from c in ContextoBD.TipoHabitacion
                                            select c;

            return resultadoTipoHabitaciones.ToList();
        }

        public void AgregarPromocion(GestorDeHotel.Model.Promociones promociones)
        {
                   
            ContextoBD.Add(promociones);
            ContextoBD.SaveChanges();

        }

        public void AgregarTipoHabitacion(GestorDeHotel.Model.TipoHabitacion tipoHabitacion)
        {
            ContextoBD.TipoHabitacion.Add(tipoHabitacion);
            ContextoBD.SaveChanges();
           

        }

        public void EditarTipoHabitacion(GestorDeHotel.Model.TipoHabitacion tipoHabitacion)
        {

            GestorDeHotel.Model.TipoHabitacion elTipoDeHabitacionAModificar;
            elTipoDeHabitacionAModificar = ObtenerTipoHabitacionPorId(tipoHabitacion.Id);

            elTipoDeHabitacionAModificar.Nombre = tipoHabitacion.Nombre;
         

            ContextoBD.Entry(elTipoDeHabitacionAModificar).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            ContextoBD.TipoHabitacion.Update(elTipoDeHabitacionAModificar);
            ContextoBD.SaveChanges();

        }

        public void EditarPromocion(GestorDeHotel.Model.Promociones promociones)
        {

            GestorDeHotel.Model.Promociones laPromocionAModificar;
            laPromocionAModificar = ObtenerPromocionPorId(promociones.Id);

            laPromocionAModificar.IdTipoDeHabitacion = promociones.IdTipoDeHabitacion;
            laPromocionAModificar.Nombre = promociones.Nombre;
            laPromocionAModificar.Desde = promociones.Desde;
            laPromocionAModificar.Hasta = promociones.Hasta;
            laPromocionAModificar.PorcentajeDeDescuento = promociones.PorcentajeDeDescuento;

            ContextoBD.Entry(laPromocionAModificar).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            ContextoBD.Promociones.Update(laPromocionAModificar);
            ContextoBD.SaveChanges();

        }

        public List<InformacionDePromociones> ListarPromociones()
        {
            var resultadoPromociones = from c in ContextoBD.Promociones
                                       join d in ContextoBD.TipoHabitacion on c.IdTipoDeHabitacion equals d.Id
                                       select new InformacionDePromociones
                                       {
                                           Id = c.Id,
                                           Nombre = c.Nombre,
                                           Desde = c.Desde,
                                           Hasta = c.Hasta,
                                           PorcentajeDeDescuento = c.PorcentajeDeDescuento,
                                           TipoDeHabitacion = d.Nombre,
                                           IdTipoDeHabitacion = c.IdTipoDeHabitacion
                                        

                                       };


          

            return resultadoPromociones.ToList();
        }

        public TipoHabitacion ObtenerTipoHabitacionPorId(int id)
        {
            GestorDeHotel.Model.TipoHabitacion resultado;

            resultado = ContextoBD.TipoHabitacion.Find(id);

            return resultado;
        }

        public Promociones ObtenerPromocionPorId(int id)
        {
            GestorDeHotel.Model.Promociones lapromocion;
           

            lapromocion = ContextoBD.Promociones.Find(id);

            return lapromocion;
        }   

        public InformacionDePromociones DetallesDePromocion(int id)
        {

            InformacionDePromociones DetallesDeLaPromocion;

            List<InformacionDePromociones> laListaDePromociones = ListarPromociones();

            foreach (var item in laListaDePromociones)
            {
                if (item.Id == id)
                {

                    DetallesDeLaPromocion = item;

                    return DetallesDeLaPromocion;

                }

            }

            return null;

        }    

        public void AgregarReservacion(Model.ReservacionViewModelCreate laReservacion)
        {
            Model.Reservaciones nuevaReservacion = new Model.Reservaciones();

            nuevaReservacion.Id_Habitacion = laReservacion.Id_Habitacion;
            nuevaReservacion.CantidadDeHoras = laReservacion.CantidadDeHoras;
            nuevaReservacion.Estado = EstadoDeReservacion.EnProceso;
            nuevaReservacion.FechaDeEntrada = DateTime.Now;
            nuevaReservacion.FechaDeSalida = nuevaReservacion.FechaDeEntrada.AddHours(nuevaReservacion.CantidadDeHoras);

           

            ContextoBD.Reservaciones.Add(nuevaReservacion);
            ContextoBD.SaveChanges();

           


        }
     
        public Model.Reservaciones EntregarHabitacion(int id)
        {
            List<Reservaciones> listaDeReservaciones;

            Model.Reservaciones ReservacionEntregada = new Model.Reservaciones();

            listaDeReservaciones = ListarTablaReserva();

            ReservacionEntregada = listaDeReservaciones.Find(e => e.Id == id);

            ReservacionEntregada.Estado = EstadoDeReservacion.Entregada;
           

            ContextoBD.Reservaciones.Update(ReservacionEntregada);
            ContextoBD.SaveChanges();

            return ReservacionEntregada;
        }

       public List<Model.InformacionDeHabitacion> ReservarHabitacion(List<InformacionDeHabitacion> listaDeHabitaciones)
        {
            List<InformacionReserva> listaDeReservas = ListarReservaciones(); 

            foreach (var itemHabitaciones in listaDeHabitaciones)
            {

                foreach (var itemReserva in listaDeReservas)
                {

                    if (itemHabitaciones.Id == itemReserva.Id_Habitacion && itemReserva.EstadoReservacion == EstadoDeReservacion.EnProceso)
                        
                    {
                        itemHabitaciones.Reservacion = true;


                    }

                }


            }

            return listaDeHabitaciones;

        }

      public Model.ReservacionViewModelDetalles DetallesReservacion(int id)
        {


            Model.Habitacion laHabitacion;

            List<Tarifa> misTarifas = ListarTarifas();

            Model.InformacionReserva reservacion;

            List<InformacionReserva> listaDeReservaciones = ListarReservas();          

            List<InformacionDeHabitacion> listaDeHabitaciones = ListarHabitaciones();

            reservacion = listaDeReservaciones.Find(e => e.Id == id);

            List<InformacionDePromociones> misPromociones = ListarPromociones();

            Model.ReservacionViewModelDetalles detallesDeReservacion = new Model.ReservacionViewModelDetalles();

            laHabitacion = ContextoBD.Habitacion.Find(reservacion.Id_Habitacion);

            detallesDeReservacion.FechaDeEntrada = DateTime.Now;
            detallesDeReservacion.FechaDeSalida = DateTime.Now.AddHours(reservacion.CantidadDeHoras);
            detallesDeReservacion.CantidadDeHoras = reservacion.CantidadDeHoras;
            detallesDeReservacion.montoDescuento = 0;
            detallesDeReservacion.IdTipoDeHabitacion = laHabitacion.IdTipoHabitacion;
            detallesDeReservacion.FechaDeSalida = reservacion.FechaDeSalida;


            foreach (var item in misTarifas)
            {

                if (DateTime.Now >= item.Desde && item.Hasta >= detallesDeReservacion.FechaDeSalida)
                {

                    detallesDeReservacion.montoPorHora = item.MontoXHora; 


                }

            }


            foreach (var itemPromociones in misPromociones)
            {


                if (itemPromociones.IdTipoDeHabitacion == detallesDeReservacion.IdTipoDeHabitacion)
                {

                    if (DateTime.Now >= itemPromociones.Desde && itemPromociones.Hasta >= detallesDeReservacion.FechaDeSalida)
                    {

                        detallesDeReservacion.porcentajeDescuento = itemPromociones.PorcentajeDeDescuento;

                    }

                }

            }



            return calcularTotalDeReservacion(detallesDeReservacion);


           

        }

      public  Model.ReservacionViewModelDetalles calcularTotalDeReservacion(Model.ReservacionViewModelDetalles reservacionDetalles)
        {
            Model.ReservacionViewModelDetalles reservacionAMostrar = reservacionDetalles;

            int totalSinDescuento = 0;

            int Descuento = 0;

            int total = 0;


            totalSinDescuento = (int)(reservacionDetalles.montoPorHora * reservacionDetalles.CantidadDeHoras);

            if (reservacionDetalles.porcentajeDescuento != 0)
            {

                Descuento = (totalSinDescuento * reservacionDetalles.porcentajeDescuento)/100;

                total = totalSinDescuento - Descuento;



            }
            else
            {
                total = totalSinDescuento;
            }

            reservacionAMostrar.montoDescuento = Descuento;
            reservacionAMostrar.montoTotalDeLaReserva = total;

            return reservacionAMostrar;

        }

      public ReservacionViewModelDetalles CheckOut (int id)
        {
           
            Model.Reservaciones laReserva;
         
            ReservacionViewModelDetalles habitacionADesocupar;

            habitacionADesocupar = DetallesDeCheckOut(id);


            laReserva = EntregarHabitacion(id);

            habitacionADesocupar.FechaDeEntrada = laReserva.FechaDeEntrada;
            habitacionADesocupar.FechaDeSalida = laReserva.FechaDeSalida;

            return habitacionADesocupar;



        }

      public Model.ReservacionViewModelDetalles DetallesDeCheckOut(int id)
        {

            List<Reservaciones> listaDeReservaciones;

            Model.ReservacionViewModelDetalles detallesReserva;

            Model.Reservaciones Reservacion;

            listaDeReservaciones = ListarTablaReserva();

            Reservacion = listaDeReservaciones.Find(e => e.Id == id);

            detallesReserva = DetallesReservacion(Reservacion.Id);

            return detallesReserva;





        }

      public int ObtenerReserva()
        {
            Model.Reservaciones LaReservacion;
            List<Reservaciones> listaDeReservaciones = ListarTablaReserva();


            LaReservacion = listaDeReservaciones.Last();

            return LaReservacion.Id;


        }


    }


    }



