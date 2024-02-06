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
    public class TarifaController : ControllerBase
    {

        private readonly GestorDeHotel.BS.IServiciosParaHotel servicios;

        public TarifaController(GestorDeHotel.BS.IServiciosParaHotel servicios)
        {
            this.servicios = servicios;
        }



        // GET: api/<TarifaController>
        [HttpGet]
        public IEnumerable<GestorDeHotel.Model.Tarifa> ObtenerTarifas()
        {

            List<GestorDeHotel.Model.Tarifa> laListaDeTarifas;
            laListaDeTarifas = servicios.ListarTarifas();

            return laListaDeTarifas;


        }

        // GET api/<TarifaController>/5
        [HttpGet("{id}")]
        public GestorDeHotel.Model.Tarifa ObtenerTarifa(int id)
        {
            GestorDeHotel.Model.Tarifa laTarifaAMostrar;

            laTarifaAMostrar = servicios.ObtenerTarifaPorId(id);
            return laTarifaAMostrar;
        }

        // POST api/<TarifaController>
        [HttpPost]
        public IActionResult Post([FromBody] GestorDeHotel.Model.Tarifa tarifa)
        {
                if (ModelState.IsValid)
                {

                    servicios.AgregarTarifa(tarifa);
                    return Ok(tarifa);

                }
                else
                {
                    return BadRequest(ModelState);
                }

        }

        // PUT api/<TarifaController>/5
        [HttpPut()]
        public IActionResult Put([FromBody] GestorDeHotel.Model.Tarifa tarifa)
        {

            if (ModelState.IsValid)
            {

                servicios.EditarTarifa(tarifa);
                return Ok(tarifa);

            }
            else
            {
                return BadRequest(ModelState);
            }

        }

      
    }
}
