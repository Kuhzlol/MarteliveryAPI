using MarteliveryAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MarteliveryAPI.Data
{
    //Add-Migration "Commentaire"
    //Update-Database
    public class DataContext(DbContextOptions options) : IdentityDbContext<User>(options)
    {
        public DbSet<Parcel> Parcels { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<CarrierRating> CarrierRatings { get; set; }
        //public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Parcel>().ToTable("parcels");
            modelBuilder.Entity<Quote>().ToTable("quotes");
            modelBuilder.Entity<Delivery>().ToTable("deliveries");
            modelBuilder.Entity<Payment>().ToTable("payments");
            modelBuilder.Entity<CarrierRating>().ToTable("carrier_ratings");

            //rename the ASP.NET Identity table names
            modelBuilder.Entity<User>(entity => { entity.ToTable(name: "users"); });
            modelBuilder.Entity<IdentityRole>(entity => { entity.ToTable(name: "roles"); });
            modelBuilder.Entity<IdentityUserRole<string>>(entity => { entity.ToTable(name: "user_roles"); });
            modelBuilder.Entity<IdentityUserClaim<string>>(entity => { entity.ToTable(name: "user_claims"); });
            modelBuilder.Entity<IdentityUserLogin<string>>(entity => { entity.ToTable(name: "user_logins"); });
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity => { entity.ToTable(name: "role_claims"); });
            modelBuilder.Entity<IdentityUserToken<string>>(entity => { entity.ToTable(name: "user_tokens"); });
        }
    }
}