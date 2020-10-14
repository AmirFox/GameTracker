using System;
using System.Collections.Generic;
using GameTracker.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace GameTracker.Contexts
{
    public class GameTrackerContext : DbContext
    {
        public DbSet<Game> Games { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=gametracker.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>().Property(g => g.Platforms).HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<IEnumerable<string>>(v));
        }
    }
}
