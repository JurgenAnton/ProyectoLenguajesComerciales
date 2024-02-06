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
    public class TipoHabitacionController : ControllerBase
    {

        private readonly GestorDeHotel.BS.IServiciosParaHotel servicios;

        public TipoHabitacionController(GestorDeHotel.BS.IServiciosParaHotel servicios)
        {
            this.servicios = servicios;
        }

        // GET: api/<TipoHabitacionController>
        [HttpGet]
        public IEnumerable<GestorDeHotel.Model.TipoHabitacion> ObtenerTipoHabitaciones()
        {

            List<GestorDeHotel.Model.TipoHabitacion> laListaDeTipoHabitacion;
            laListaDeTipoHabitacion = servicios.ListarTipoHabitaciones();

            return laListaDeTipoHabitacion;

        }

        // GET api/<TipoHabitacionController>/5
        [HttpGet("{id}")]
        public GestorDeHotel.Model.TipoHabitacion ObtenerTipoHabitacion(int id)
        {
            GestorDeHotel.Model.TipoHabitacion elTipoHabitacionAMostrar;

            elTipoHabitacionAMostrar = servicios.ObtenerTipoHabitacionPorId(id);
            return elTipoHabitacionAMostrar;
        }

        // POST api/<TipoHabitacionController>
        [HttpPost]
        public IActionResult Post([FromBody] GestorDeHotel.Model.TipoHabitacion tipoHabitacion)
        {

            if (ModelState.IsValid)
            {

                servicios.AgregarTipoHabitacion(tipoHabitacion);
                return Ok(tipoHabitacion);

            }
            else
            {
                return BadRequest(ModelState);
            }


        }

        // PUT api/<TipoHabitacionController>/5
        [HttpPut()]
        public IActionResult Put([FromBody] GestorDeHotel.Model.TipoHabitacion tipoHabitacion)
        {


            if (ModelState.IsValid)
            {

                servicios.EditarTipoHabitacion(tipoHabitacion);
                return Ok(tipoHabitacion);

            }
            else
            {
                return BadRequest(ModelState);
            }

        }

      
    }
}
