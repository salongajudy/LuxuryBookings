using BookingPlatform.DAL;
using BookingPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

namespace BookingPlatform.api
{
    [RoutePrefix("api/Bookings")]
    public class BookingsController : ApiController
    {
        private DefaultContext db = new DefaultContext();

        [Route("GetBooking")]
        public string Get(string booking)
        {
            string result = string.Empty;
            Bookings resBooking = new Bookings();
            Bookings paramBooking = new Bookings();
            paramBooking = JsonConvert.DeserializeObject<Bookings>(booking);
            resBooking = db.Bookings.Where(m => m.RoomId == paramBooking.RoomId && (m.DateFrom <= paramBooking.DateTo && paramBooking.DateFrom <= m.DateTo)).FirstOrDefault();
            if(resBooking != null)
            {
                result = "Room is not available on the dates selected.";
            }
            else
            {
                if(paramBooking.PersonNo > paramBooking.Room.Capacity)
                {
                    result = "Visitor is greater than the capacity of the room.";
                }
                else
                {
                    if(paramBooking.DateFrom > paramBooking.DateTo) result = "Date from is greater than Date to.";
                }

            }
            return result;
        }
        [Route("SaveBooking")]
        // POST: api/Bookings
        public string Post([FromBody]Bookings booking)
        {
            string result = string.Empty;
            try
            {
                //Bookings paramBooking = new Bookings();
                //paramBooking = JsonConvert.DeserializeObject<Bookings>(booking);
                db.Bookings.Add(booking);
                db.SaveChanges();
                
            }
            catch(Exception e)
            {
                result = e.Message.ToString();
            }
            
            return result;
        }

      
    }
}
