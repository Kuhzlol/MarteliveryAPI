using MarteliveryAPI_DotNet8_v01.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MarteliveryAPI_DotNet8_v01.Data
{
    //Add-Migration "Commentaire"
    //Update-Database
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {
        
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Parcel> Parcels { get; set; }
        public DbSet<Carrier> Carriers { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<CarrierRating> CarrierRatings { get; set; }
        public DbSet<TestUser> TestUsers { get; set; }

    }
}