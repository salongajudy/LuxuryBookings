using BookingPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace BookingPlatform.Controllers
{
    public class HomeController : Controller
    {
        private BL _bl = new BL();
        public async Task<ActionResult> Index()
        {
            List<Rooms> liRooms = new List<Rooms>();

            //Sending request to find web api REST service resource Get using HttpClient  
            HttpResponseMessage Res = await _bl.CallHttpClient("api/Rooms/GetAllRooms");

            //Checking the response is successful or not which is sent using HttpClient  
            if (Res.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api   
                var roomResponse = Res.Content.ReadAsStringAsync().Result;

                //Deserializing the response recieved from web api and storing into the Room list  
                liRooms = JsonConvert.DeserializeObject<List<Rooms>>(roomResponse);

            }
            return View(liRooms);
        }
        public async Task<ActionResult> LoadPartial(int roomId)
        {
            Bookings newBooking = new Bookings();
            newBooking.RoomId = roomId;
            Rooms room = new Rooms();
            HttpResponseMessage Res = await _bl.CallHttpClient(string.Format("api/Rooms/GetRoom?id={0}", roomId));
            if (Res.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api   
                var roomResponse = Res.Content.ReadAsStringAsync().Result;
                //Deserializing the response recieved from web api and storing into the Room  
                room = JsonConvert.DeserializeObject<Rooms>(roomResponse);

            }
            newBooking.Room = room;
            return PartialView("_Booking", newBooking);
        }
        public async Task<ActionResult> SaveBooking(Bookings booking)
        {
            var result = new object();
            try
            {
                HttpResponseMessage Res = new HttpResponseMessage();
                //Check if Model is valid
                if(!ModelState.IsValid)
                {
                    string errorMsg = string.Empty;
                    foreach (ModelState modelState in ViewData.ModelState.Values)
                    {
                        foreach (ModelError error in modelState.Errors)
                        {
                            errorMsg += error.ErrorMessage + @"<br \>";
                        }
                    }
                    result = new { isError = true, msg = errorMsg };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                //Validate Room Availability and capacity first

                Res = await _bl.CallHttpClient(string.Format("api/Bookings/GetBooking?booking={0}", JsonConvert.SerializeObject(booking)));
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var validationResult = JsonConvert.DeserializeObject<string>(Res.Content.ReadAsStringAsync().Result);
                    result = new { isError = true, msg = validationResult };
                    if (string.IsNullOrEmpty(validationResult))
                    {
                        Res = await _bl.PostHttpClient("api/Bookings/SaveBooking", booking);
                        if (Res.IsSuccessStatusCode)
                        {
                            validationResult = JsonConvert.DeserializeObject<string>(Res.Content.ReadAsStringAsync().Result);
                            if (string.IsNullOrEmpty(validationResult))
                            {
                                result = new { isError = false, msg = "Booking has been successfully saved." };
                            }
                                
                        }
                    }
                    
                }
                else
                {
                    result = new { isError = true, msg = Res.RequestMessage };
                }
            }
            catch (Exception e)
            {
                result = new { isError = true, msg = "ERROR: " + e.ToString() };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
    internal class BL
    {
        private string Baseurl = "http://localhost:7046/";
        public async Task<HttpResponseMessage> CallHttpClient(string apiRoute)
        {
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource Get using HttpClient  
                HttpResponseMessage Res = await client.GetAsync(apiRoute);

                return Res;
            }
        }
        public async Task<HttpResponseMessage> PostHttpClient(string apiRoute, Bookings booking)
        {
            using (var client = new HttpClient())
            {
                //Passing service base url  
                Uri baseAddress = new Uri(Baseurl+ apiRoute);
                client.BaseAddress = baseAddress;

                client.DefaultRequestHeaders.Accept.Clear();
               
                //Sending request to find web api REST service resource Post using HttpClient  
                HttpResponseMessage Res = await client.PostAsJsonAsync(baseAddress.ToString(), booking);

                return Res;
            }
        }

    }
}