﻿using System.Linq;
using Microsoft.EntityFrameworkCore;
using SmartPlate.API.Models;
using SmartPlate.API.Models.Users;

namespace SmartPlate.API.Db
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Officer> Officers { get; set; }
        public DbSet<TrafficUser> TrafficUsers { get; set; }

        public DbSet<Traffic> Traffics { get; set; }

        public DbSet<Car> Cars { get; set; }

        public DbSet<StolenCar> StolenCars { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
            var pendingMigrations = Database.GetPendingMigrations();
            if (pendingMigrations.Any())
            {
                Database.MigrateAsync().Wait();
            }
        }

    }
}