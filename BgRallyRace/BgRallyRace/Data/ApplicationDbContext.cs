using System;
using System.Collections.Generic;
using System.Text;
using BgRallyRace.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BgRallyRace.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
         : base(options)
        {

        }
        public ApplicationDbContext()
        {

        }

        public DbSet<Opinions> Opinions { get; set; }
        public DbSet<UserRequest> UserRequests { get; set; }
        public DbSet<Competitions> Competitions { get; set; }
        public DbSet<RallyTracks> RallyTracks { get; set; }
        public DbSet<Teams> Teams { get; set; }
        public DbSet<MoneyAccount> MoneyAccount { get; set; }
        public DbSet<RallyPilots> RallyPilots { get; set; }
        public DbSet<RallyNavigators> RallyNavigators { get; set; }
        public DbSet<RallyFitters> RallyFitters { get; set; }
        public DbSet<Cars> Cars { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);  

            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=SUPER-HP\\SQLEXPRESS;Database=BgRallyRace;Trusted_Connection=True;");
            }
        }

    }
}
