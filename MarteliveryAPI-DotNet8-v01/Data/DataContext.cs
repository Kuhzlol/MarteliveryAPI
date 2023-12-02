﻿using MarteliveryAPI_DotNet8_v01.Entities;
using Microsoft.EntityFrameworkCore;

namespace MarteliveryAPI_DotNet8_v01.Data
{
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
    }
}
