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
        public DbSet<Opinions> Opinions { get; set; }
        public DbSet<UserRequest> UserRequests { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
