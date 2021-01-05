﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingPlatform.Models
{
    public class Rooms
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public int Capacity { get; set; }
    }
}