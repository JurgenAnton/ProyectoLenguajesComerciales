using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestorDeHotel.SI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HabitacionController : ControllerBase
    {

        private readonly BS.IServiciosParaHotel Servicios;

        public HabitacionController(BS.IServiciosParaHotel servicios)
        {
            Servicios = servicios;
        }


        // GET: api/<ValuesController>
        [HttpGet("ObtenerHabitaciones")]
        public List<Model.InformacionDeHabitacion> ObtenerHabitaciones()
        {

            List<Model.InformacionDeHabitacion> laListaDeHabitaciones;
            List<Model.InformacionDeHabitacion> laListaDeHabitacionesActualizada;

            laListaDeHabitaciones = Servicios.ListarHabitaciones();

            laListaDeHabitacionesActualizada = Servicios.ReservarHabitacion(laListaDeHabitaciones);

            return laListaDeHabitacionesActualizada;

        }

        [HttpGet("LlenarDropDownList")]
        public Model.InformacionDeHabitacion LlenarDropDownList()
        {

            var modelHabitacionView = new Model.InformacionDeHabitacion();
            modelHabitacionView.ListaDeTiposHabitacion = Servicios.ListarTipoHabitaciones();


            return modelHabitacionView;

        }


        [HttpPost("AgregarHabitacion")]
        public IActionResult Post([FromBody] Model.HabitacionViewModelCreate NuevaHabitacion)
        {

            if (ModelState.IsValid)
            {
                Servicios.AgregarHabitacion(NuevaHabitacion);
                return Ok(NuevaHabitacion);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }


        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public Model.InformacionDeHabitacion Get(int id)
        {

            Model.InformacionDeHabitacion HabitacionAEditar;
            HabitacionAEditar = Servicios.BuscarHabitacionPorId(id);
            HabitacionAEditar.ListaDeTiposHabitacion = Servicios.ListarTipoHabitaciones();
            return HabitacionAEditar;

        }

        //PUT api/<HabitacionController>
        [HttpPut("Edit")]
        public IActionResult Put([FromBody] Model.HabitacionViewModelCreate HabitacionEditada)
        {
            if (ModelState.IsValid)
            {
                Servicios.EditarHabitacion(HabitacionEditada);
                return Ok(HabitacionEditada);
            }
            else
            {
                return BadRequest(ModelState);
            }


        }

        // PUT api/<ValuesController>/5
        [HttpPut("Reparar")]
        public IActionResult Put([FromBody] int id)
        {
            if (id != 0)
            {
                Servicios.Reparar(id);
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPut("Habilitar")]
        public IActionResult Habilitar([FromBody] int id)
        {
            if (id != 0)
            {
                Servicios.Habilitar(id);
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpGet("Detalles")]
        public Model.InformacionDeHabitacion Detalles(int id)
        {

            Model.InformacionDeHabitacion HabitacionADetallar;
            HabitacionADetallar = Servicios.DetallesDeHabitacion(id);
          
            return HabitacionADetallar;

        }



    }
}
