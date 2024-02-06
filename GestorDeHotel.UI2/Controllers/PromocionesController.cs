using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace GestorDeHotel.UI2.Controllers
{
    [Authorize]
    public class PromocionesController : Controller
    { 
            // GET: PromocionesController
            public async Task<IActionResult> Index()
            {
                List<GestorDeHotel.Model.InformacionDePromociones> laListaDePromociones;

                try
                {

                    var httpClient = new HttpClient();

                    var response = await httpClient.GetAsync("https://apipicateclashotel.azurewebsites.net/api/Promociones");

                    string apiResponse = await response.Content.ReadAsStringAsync();

                    laListaDePromociones = JsonConvert.DeserializeObject<List<GestorDeHotel.Model.InformacionDePromociones>>(apiResponse);

                }
                catch (Exception ex)
                {

                    throw ex;
                }


                return View(laListaDePromociones);
            }


            // GET: PromocionesController/Create
            public async Task<IActionResult> Create()
            {

                var modelPromocionView = new Model.InformacionDePromociones();

                try
                {
                    var httpClient = new HttpClient();

                    var response = await httpClient.GetAsync("https://apipicateclashotel.azurewebsites.net/api/Promociones/LlenarDropDownList");

                    string apiResponse = await response.Content.ReadAsStringAsync();

                    modelPromocionView = JsonConvert.DeserializeObject<Model.InformacionDePromociones>(apiResponse);

                }
                catch (Exception)
                {
                    throw new Exception();

                }

                return View(modelPromocionView);
            }

            // POST: PromocionesController/Create
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<ActionResult> Create(GestorDeHotel.Model.Promociones promociones)
            {

                try
                {

                    if (ModelState.IsValid)
                    {

                        var httpClient = new HttpClient();

                        string json = JsonConvert.SerializeObject(promociones);

                        var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                        var byteContent = new ByteArrayContent(buffer);

                        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                        await httpClient.PostAsync("https://apipicateclashotel.azurewebsites.net/api/Promociones/", byteContent);




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

            // GET: PromocionesController/Edit/5
            [HttpGet]
            public async Task<IActionResult> Edit(int id)
            {
                var PromocionAEditar = new Model.Promociones();

                try
                {
                    var httpClient = new HttpClient();

                    var response = await httpClient.GetAsync("https://apipicateclashotel.azurewebsites.net/api/Promociones/" + id);

                    string apiResponse = await response.Content.ReadAsStringAsync();

                    PromocionAEditar = JsonConvert.DeserializeObject<Model.Promociones>(apiResponse);

                }
                catch (Exception)
                {
                    throw new Exception();

                }

                return View(PromocionAEditar);

            }

            // POST: PromocionesController/Edit/5
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<ActionResult> Edit(GestorDeHotel.Model.Promociones promociones)
            {
                try
                {

                    if (ModelState.IsValid)
                    {

                        var httpClient = new HttpClient();

                        string json = JsonConvert.SerializeObject(promociones);

                        var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                        var byteContent = new ByteArrayContent(buffer);

                        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                        await httpClient.PutAsync("https://apipicateclashotel.azurewebsites.net/api/Promociones", byteContent);

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


            // GET: PromocionesController/Details/5
            [HttpGet]
            public async Task<IActionResult> Details(int id)
            {
                var DetallesDeLaPromocion = new Model.InformacionDePromociones();


                try
                {
                    var httpClient = new HttpClient();

                    var response = await httpClient.GetAsync("https://apipicateclashotel.azurewebsites.net/api/Promociones/Detalles?id=" + id);

                    string apiResponse = await response.Content.ReadAsStringAsync();

                    DetallesDeLaPromocion = JsonConvert.DeserializeObject<Model.InformacionDePromociones>(apiResponse);



                }
                catch (Exception)
                {
                    throw new Exception();

                }

                return View(DetallesDeLaPromocion);

            }



        }

    }

