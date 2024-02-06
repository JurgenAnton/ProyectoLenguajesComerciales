using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class ReservacionesController : Controller
    {
        // GET: ReservacionesController
        public async Task<ActionResult> Index()
        {
            List<GestorDeHotel.Model.InformacionReserva> laListaDeReservaciones;

            try
            {

                var httpClient = new HttpClient();

                var response = await httpClient.GetAsync("https://apipicateclashotel.azurewebsites.net/api/Reservaciones");

                string apiResponse = await response.Content.ReadAsStringAsync();

                laListaDeReservaciones = JsonConvert.DeserializeObject<List<GestorDeHotel.Model.InformacionReserva>>(apiResponse);

            }
            catch (Exception ex)
            {

                throw ex;
            }


            return View(laListaDeReservaciones);
        }

        public async Task<IActionResult> ObtenerIdDeReservacion(Model.ReservacionViewModelCreate laReservacion)
        {
            Model.ReservacionViewModelCreate Reservacion;

            Reservacion = laReservacion;

            try
            {

                var httpClient = new HttpClient();

                var response = await httpClient.GetAsync("https://apipicateclashotel.azurewebsites.net/api/Reservaciones/ObtenerReserva");

                string apiResponse = await response.Content.ReadAsStringAsync();

                Reservacion.Id = JsonConvert.DeserializeObject<int>(apiResponse);


                return await Detalles(Reservacion);

            }
            catch (Exception ex)
            {

                throw ex;
            }



        }



        // GET: ReservacionesController/Details/5
        public ActionResult Reservar(int idHabitacion, Boolean reservacion, Model.Estado estado)
        {
            Model.InformacionReserva laReservacion = new Model.InformacionReserva();
            laReservacion.Id_Habitacion = idHabitacion;
            laReservacion.Reservacion = reservacion;
            laReservacion.Estado = estado;

            if (laReservacion.Estado == Model.Estado.BuenasCondiciones && laReservacion.Reservacion == false)
            {

                return View(laReservacion);

            }
            else
            {

                ViewBag.Alert = "no se puede reservar una habitación ocupada o en reparación";
                return View("ReservarAlerta");

            }


        }

        // POST: ReservacionesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reservar(Model.ReservacionViewModelCreate laReservacion)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    var httpClient = new HttpClient();

                    string json = JsonConvert.SerializeObject(laReservacion);

                    var buffer = System.Text.Encoding.UTF8.GetBytes(json);

                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    await httpClient.PostAsync("https://apipicateclashotel.azurewebsites.net/api/Reservaciones/AgregarReservacion", byteContent);


                    return await ObtenerIdDeReservacion(laReservacion);






                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }



        }

        // POST: ReservacionesController/Edit/5
        [HttpGet]
        public async Task<IActionResult> Detalles(Model.ReservacionViewModelCreate laReservacion)
        {
            Model.ReservacionViewModelDetalles detallesReservacion = new Model.ReservacionViewModelDetalles();


            try
            {
                var httpClient = new HttpClient();

                var response = await httpClient.GetAsync("https://apipicateclashotel.azurewebsites.net/api/Reservaciones/Detalles?id=" + laReservacion.Id);

                string apiResponse = await response.Content.ReadAsStringAsync();

                detallesReservacion = JsonConvert.DeserializeObject<Model.ReservacionViewModelDetalles>(apiResponse);

                return View("Detalles", detallesReservacion);


            }
            catch (Exception)
            {
                throw new Exception();

            }



        }



        [HttpGet]
        public async Task<IActionResult> CheckOut(int id, Model.EstadoDeReservacion estadoDeReservacion)
        {

            if (estadoDeReservacion == Model.EstadoDeReservacion.EnProceso)
            {

                Model.ReservacionViewModelDetalles checkOutReservacion = new Model.ReservacionViewModelDetalles();

                try
                {
                    var httpClient = new HttpClient();

                    var response = await httpClient.GetAsync("https://apipicateclashotel.azurewebsites.net/api/Reservaciones/CheckOut?id=" + id);

                    string apiResponse = await response.Content.ReadAsStringAsync();

                    checkOutReservacion = JsonConvert.DeserializeObject<Model.ReservacionViewModelDetalles>(apiResponse);

                    return View("CheckOut", checkOutReservacion);


                }
                catch (Exception)
                {
                    throw new Exception();

                }

            }
            else
            {
                ViewBag.Alert = "no se puede hacer check out a una habiatación que no este reservada";
                return View("ReservarAlerta");

            }

        }


    }
}

