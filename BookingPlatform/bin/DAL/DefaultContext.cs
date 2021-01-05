using BookingPlatform.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace BookingPlatform.DAL
{
    public class DefaultContext : DbContext
    {
        public DefaultContext() : base("DefaultContext")
        {
        }
        public DbSet<Rooms> Rooms { get; set; }
        public DbSet<Bookings> Bookings { get; set; }
    }
}