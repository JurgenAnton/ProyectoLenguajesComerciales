using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestorDeHotel.SI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservacionesController : ControllerBase
    {

        private readonly GestorDeHotel.BS.IServiciosParaHotel servicios;

        public ReservacionesController(GestorDeHotel.BS.IServiciosParaHotel servicios)
        {
            this.servicios = servicios;
        }

        // GET: api/<ReservacionesController>
        [HttpGet]
        public List<GestorDeHotel.Model.InformacionReserva> ObtenerReservaciones()
        {

            List<GestorDeHotel.Model.InformacionReserva> laListaDeReservas;
            laListaDeReservas = servicios.ListarReservaciones();

            return laListaDeReservas;
            
        }


        // GET api/<ReservacionesController>/5
        [HttpGet("Detalles")]
        public Model.ReservacionViewModelDetalles Get(int id)
        {

            Model.ReservacionViewModelDetalles detallesDeRserva;
            detallesDeRserva = servicios.DetallesReservacion(id);
         
            return detallesDeRserva;

        }


        [HttpGet("CheckOut")]
        public Model.ReservacionViewModelDetalles CheckOut(int id)
        {

            Model.ReservacionViewModelDetalles ReservacionEntregada;
            ReservacionEntregada = servicios.CheckOut(id);

            return ReservacionEntregada;

        }


        // POST api/<ReservacionesController>
        [HttpPost("AgregarReservacion")]
        public IActionResult Post([FromBody] Model.ReservacionViewModelCreate laReservacion)
        {

            if (ModelState.IsValid)
            {
                servicios.AgregarReservacion(laReservacion);
                return Ok(laReservacion); 
                
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        [HttpGet("ObtenerReserva")]
        public int ObtenerIdReserva()
        {

            int IdReserva;

            IdReserva = servicios.ObtenerReserva();

            return IdReserva;

        }



    }
}
