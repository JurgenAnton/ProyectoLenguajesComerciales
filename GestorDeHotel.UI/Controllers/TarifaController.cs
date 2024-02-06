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
    public class TarifaController : Controller
    {
        // GET: TarifaController
        public async Task<IActionResult> Index()
        {
            List<GestorDeHotel.Model.Tarifa> laListaDeTarifas;

            try
            {

                var httpClient = new HttpClient();

                var response = await httpClient.GetAsync("https://localhost:44339/api/Tarifa");

                string apiResponse = await response.Content.ReadAsStringAsync();

                laListaDeTarifas = JsonConvert.DeserializeObject<List<GestorDeHotel.Model.Tarifa>>(apiResponse);

            }
            catch (Exception ex)
            {

                throw ex;
            }


            return View(laListaDeTarifas);
        }

        // GET: TarifaController/Details/5
        public async Task<ActionResult> Details(int id)
        {

            Model.Tarifa laTarifaAMostrar;

            try
            {

                var httpClient = new HttpClient();

                var response = await httpClient.GetAsync("https://localhost:44339/api/Tarifa/"+id);

                string apiResponse = await response.Content.ReadAsStringAsync();

                laTarifaAMostrar = JsonConvert.DeserializeObject<GestorDeHotel.Model.Tarifa>(apiResponse);

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return View(laTarifaAMostrar);
        }

        // GET: TarifaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TarifaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(GestorDeHotel.Model.Tarifa tarifa)
        {

            try
            {

                if (ModelState.IsValid)
                {

                    var httpClient = new HttpClient();

                    string json = JsonConvert.SerializeObject(tarifa);

                    var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    await httpClient.PostAsync("https://localhost:44339/api/Tarifa/", byteContent);




                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View();
                }
               
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: TarifaController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                Model.Tarifa TarifaAEditar = new Model.Tarifa();

                var httpClient = new HttpClient();

                var response = await httpClient.GetAsync("https://localhost:44339/api/Tarifa/"+ id);

                string apiResponse = await response.Content.ReadAsStringAsync();

                TarifaAEditar = JsonConvert.DeserializeObject<GestorDeHotel.Model.Tarifa>(apiResponse);

                return View(TarifaAEditar);
            }
            catch (Exception)
            {

                throw new Exception();
            }

           
        }

        // POST: TarifaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(GestorDeHotel.Model.Tarifa tarifa)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    var httpClient = new HttpClient();

                    string json = JsonConvert.SerializeObject(tarifa);

                    var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    await httpClient.PutAsync("https://localhost:44339/api/Tarifa", byteContent);

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View();
                }


            }catch(Exception ex)
            {
                throw ex;
            }

            }

    }
}
