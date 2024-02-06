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
    public class PromocionesController : ControllerBase
    {
        private readonly GestorDeHotel.BS.IServiciosParaHotel servicios;

        public PromocionesController(GestorDeHotel.BS.IServiciosParaHotel servicios)
        {
            this.servicios = servicios;
        }

        // GET: api/<PromocionesController>
        [HttpGet]
        public List<GestorDeHotel.Model.InformacionDePromociones> ObtenerPromociones()
        {
            List<GestorDeHotel.Model.InformacionDePromociones> laListaDePromociones;
            laListaDePromociones = servicios.ListarPromociones();

            return laListaDePromociones;
        }


        [HttpGet("LlenarDropDownList")]
        public Model.InformacionDePromociones LlenarDropDownList()
        {

            var modelPromocionView = new Model.InformacionDePromociones();
            modelPromocionView.ListaDeTiposHabitacion = servicios.ListarTipoHabitaciones();


            return modelPromocionView;

        }






        // GET api/<PromocionesController>/5
        [HttpGet("{id}")]
        public GestorDeHotel.Model.Promociones Get(int id)
        {
            GestorDeHotel.Model.Promociones laPromocionAMostrar;

            laPromocionAMostrar = servicios.ObtenerPromocionPorId(id);

            return laPromocionAMostrar;
        }

        // POST api/<PromocionesController>
        [HttpPost]
        public IActionResult Post([FromBody] GestorDeHotel.Model.Promociones promociones)
        {
            if (ModelState.IsValid)
            {

                servicios.AgregarPromocion(promociones);
                return Ok(promociones);

            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // PUT api/<PromocionesController>/5
        [HttpPut()]
        public IActionResult Put([FromBody] GestorDeHotel.Model.Promociones promociones)
        {
            
        
                if (ModelState.IsValid)
            {

                servicios.EditarPromocion(promociones);
                return Ok(promociones);

            }
            else
            {
                return BadRequest(ModelState);
            }
        }



        [HttpGet("Detalles")]
        public Model.InformacionDePromociones Detalles(int id)
        {

            Model.InformacionDePromociones PromocionADetallar;
            PromocionADetallar = servicios.DetallesDePromocion(id);

            return PromocionADetallar;

        }

    }
}
