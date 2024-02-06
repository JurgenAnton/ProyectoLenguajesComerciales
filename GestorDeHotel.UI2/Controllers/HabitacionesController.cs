using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;

namespace GestorDeHotel.UI2.Controllers
{
    [Authorize]
    public class HabitacionesController : Controller
    {

        // GET: HabitacionesController
        public async Task<IActionResult> Index()
        {
            List<Model.InformacionDeHabitacion> laListaDeHabitaciones;


            try
            {
                var httpClient = new HttpClient();

                var response = await httpClient.GetAsync("https://apipicateclashotel.azurewebsites.net/api/Habitacion/ObtenerHabitaciones");

                string apiResponse = await response.Content.ReadAsStringAsync();

                laListaDeHabitaciones = JsonConvert.DeserializeObject<List<Model.InformacionDeHabitacion>>(apiResponse);

            }
            catch (Exception)
            {

                throw new Exception();


            }


            return View(laListaDeHabitaciones);
        }


        // GET: HabitacionesController/LLenarListaDeTiposDeHabitaciones
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var modelHabitacionView = new Model.InformacionDeHabitacion();

            try
            {
                var httpClient = new HttpClient();

                var response = await httpClient.GetAsync("https://apipicateclashotel.azurewebsites.net/api/Habitacion/LlenarDropDownList");

                string apiResponse = await response.Content.ReadAsStringAsync();

                modelHabitacionView = JsonConvert.DeserializeObject<Model.InformacionDeHabitacion>(apiResponse);

            }
            catch (Exception)
            {
                throw new Exception();

            }

            return View(modelHabitacionView);
        }

        // POST: HabitacionesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Model.HabitacionViewModelCreate NuevaHabitacion)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var httpClient = new HttpClient();

                    string json = JsonConvert.SerializeObject(NuevaHabitacion);

                    var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    await httpClient.PostAsync("https://apipicateclashotel.azurewebsites.net/api/Habitacion/AgregarHabitacion", byteContent);


                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return BadRequest(ModelState);

                }

            }
            catch (Exception)
            {
                throw new Exception();
            }
        }



        // GET: HabitacionesController/Edit/
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var habitacionAEditar = new Model.InformacionDeHabitacion();

            try
            {
                var httpClient = new HttpClient();

                var response = await httpClient.GetAsync("https://apipicateclashotel.azurewebsites.net/api/Habitacion/" + id);

                string apiResponse = await response.Content.ReadAsStringAsync();

                habitacionAEditar = JsonConvert.DeserializeObject<Model.InformacionDeHabitacion>(apiResponse);

            }
            catch (Exception)
            {
                throw new Exception();

            }

            return View(habitacionAEditar);

        }

        // PUT: HabitacionesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Model.HabitacionViewModelCreate HabitacionEditada)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var httpClient = new HttpClient();

                    string json = JsonConvert.SerializeObject(HabitacionEditada);

                    var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    await httpClient.PutAsync("https://apipicateclashotel.azurewebsites.net/api/Habitacion/Edit", byteContent);

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {
                throw new Exception();

            }

        }

        //HabitacionesController
        [Route("Reparar/{id}")]
        public async Task<IActionResult> Reparar(int id, Boolean reserva)
        {
            try
            {

                if (id != 0 && reserva == false)
                {
                    var httpClient = new HttpClient();

                    string json = JsonConvert.SerializeObject(id);

                    var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    await httpClient.PutAsync("https://apipicateclashotel.azurewebsites.net/api/Habitacion/Reparar", byteContent);


                    return RedirectToAction(nameof(Index));
                }
                else
                {

                    ViewBag.Alert = "no se puede reparar, debido a que hay una reservación vigente";
                    return View("RepararAlerta");


                }
            }
            catch (Exception)
            {
                throw new Exception();
            }

        }




        [Route("Habilitar/{id}")]
        public async Task<IActionResult> Habilitar(int id)
        {
            try
            {

                if (id != 0)
                {
                    var httpClient = new HttpClient();

                    string json = JsonConvert.SerializeObject(id);

                    var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    await httpClient.PutAsync("https://apipicateclashotel.azurewebsites.net/api/Habitacion/Habilitar", byteContent);


                    return RedirectToAction(nameof(Index));
                }
                else
                {

                    return BadRequest();


                }
            }
            catch (Exception)
            {
                throw new Exception();
            }

        }




        [HttpGet]
        public async Task<IActionResult> Detalles(int id)
        {
            var DetallesDeLaHabitacion = new Model.InformacionDeHabitacion();


            try
            {
                var httpClient = new HttpClient();

                var response = await httpClient.GetAsync("https://apipicateclashotel.azurewebsites.net/api/Habitacion/Detalles?id=" + id);

                string apiResponse = await response.Content.ReadAsStringAsync();

                DetallesDeLaHabitacion = JsonConvert.DeserializeObject<Model.InformacionDeHabitacion>(apiResponse);



            }
            catch (Exception)
            {
                throw new Exception();

            }

            return View(DetallesDeLaHabitacion);

        }


    }

}

