using MarteliveryAPI_DotNet8_v01.Models;
using Microsoft.EntityFrameworkCore;

namespace MarteliveryAPI_DotNet8_v01.Data
{
    //Add-Migration "Commentaire"
    //Update-Database
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to postgres with connection string from app settings
            options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Parcel> Parcels { get; set; }
        public DbSet<Carrier> Carriers { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<CarrierRating> CarrierRatings { get; set; }

    }
}