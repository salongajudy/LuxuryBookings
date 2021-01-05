using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BookingPlatform.DAL;
using BookingPlatform.Models;

namespace BookingPlatform.api
{
    [RoutePrefix("api/Rooms")]
    public class RoomsController : ApiController
    {
        private DefaultContext db = new DefaultContext();
        // GET: api/Rooms
        [Route("GetAllRooms")]
        public IEnumerable<Rooms> Get()
        {
            return db.Rooms.ToList();
        }
        [Route("GetRoom")]
        // GET: api/Rooms/5
        public Rooms Get(int id)
        {
            return db.Rooms.Where(m => m.Id == id).FirstOrDefault();
        }

    }
}
