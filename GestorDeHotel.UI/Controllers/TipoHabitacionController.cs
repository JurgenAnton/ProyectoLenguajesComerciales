using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GestorDeHotel.UI.Controllers
{
    public class TipoHabitacionController : Controller
    {
        // GET: TipoHabitacionController
        public async Task<ActionResult> IndexAsync()
        {
            List<GestorDeHotel.Model.TipoHabitacion> laListaDeTipoHabtaciones;

            try
            {

                var httpClient = new HttpClient();

                var response = await httpClient.GetAsync("https://localhost:44339/api/TipoHabitacion");

                string apiResponse = await response.Content.ReadAsStringAsync();

                laListaDeTipoHabtaciones = JsonConvert.DeserializeObject<List<GestorDeHotel.Model.TipoHabitacion>>(apiResponse);

            }
            catch (Exception ex)
            {

                throw ex;
            }


            return View(laListaDeTipoHabtaciones);
        }

        // GET: TipoHabitacionController/Details/5
        public async Task<ActionResult> DetailsAsync(int id)
        {
            Model.TipoHabitacion elTipoHabitacionAMostrar;

            try
            {

                var httpClient = new HttpClient();

                var response = await httpClient.GetAsync("https://localhost:44339/api/TipoHabitacion/" + id);

                string apiResponse = await response.Content.ReadAsStringAsync();

                elTipoHabitacionAMostrar = JsonConvert.DeserializeObject<GestorDeHotel.Model.TipoHabitacion>(apiResponse);

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return View(elTipoHabitacionAMostrar);
        }

        // GET: TipoHabitacionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoHabitacionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Model.TipoHabitacion tipoHabitacion)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    var httpClient = new HttpClient();

                    string json = JsonConvert.SerializeObject(tipoHabitacion);

                    var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    await httpClient.PostAsync("https://localhost:44339/api/TipoHabitacion/", byteContent);




                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View();
                }

            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: TipoHabitacionController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {

            Model.TipoHabitacion elTipoHabitacionAEditar;

            try
            {

                var httpClient = new HttpClient();

                var response = await httpClient.GetAsync("https://localhost:44339/api/TipoHabitacion/" + id);

                string apiResponse = await response.Content.ReadAsStringAsync();

                elTipoHabitacionAEditar = JsonConvert.DeserializeObject<GestorDeHotel.Model.TipoHabitacion>(apiResponse);

                return View(elTipoHabitacionAEditar);

            }
            catch (Exception ex)
            {

                throw ex;
            }           
        }



        // POST: TipoHabitacionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(GestorDeHotel.Model.TipoHabitacion tipoHabitacion)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    var httpClient = new HttpClient();

                    string json = JsonConvert.SerializeObject(tipoHabitacion);

                    var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    await httpClient.PutAsync("https://localhost:44339/api/TipoHabitacion", byteContent);

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View();
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

        
    }

    }
}
