namespace BgRallyRace.Data
{
    using BgRallyRace.Models;
    using BgRallyRace.Models.Competitions;
    using BgRallyRace.Models.Home;
    using BgRallyRace.Models.Money;
    using BgRallyRace.Models.RandomName;
    using BgRallyRace.Models.Teams;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using System;

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
        public DbSet<PartsCars> PartsCars { get; set; }
        public DbSet<Competitions> Competitions { get; set; }
        public DbSet<RallyRunway> RallyRunways { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<MoneyAccount> MoneyAccount { get; set; }
        public DbSet<RallyPilots> RallyPilots { get; set; }
        public DbSet<RallyNavigators> RallyNavigators { get; set; }
        public DbSet<RallyFitters> RallyFitters { get; set; }
        public DbSet<Cars> Cars { get; set; }
        public DbSet<CompetitionsTeams> CompetitionsTeam { get; set; }
        public DbSet<Aerodynamics> Aerodynamics { get; set; }
        public DbSet<Brakes> Brakes { get; set; }
        public DbSet<Engines> Engines { get; set; }
        public DbSet<Gearboxs> Gearboxs { get; set; }
        public DbSet<ModelsCars> ModelsCars { get; set; }
        public DbSet<Mountings> Mountings { get; set; }
        public DbSet<Turbo> Turbos { get; set; }
        public DbSet<CompetitionsRallyRunway> CompetitionsRallyRunway { get; set; }
        public DbSet<FirstNames> FirstNames { get; set; }
        public DbSet<LastNames> LastNames { get; set; }
        public DbSet<FinancialStatistics> FinancialStatistics { get; set; }
        public DbSet<RaceHistory> RaceHistories { get; set; }
        public DbSet<RatingList> RatingLists { get; set; }
        public DbSet<FAQ> FAQs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CompetitionsTeams>().HasKey(sc => new { sc.CompetitionId, sc.TeamId });

            builder.Entity<CompetitionsTeams>()
                .HasOne<Competitions>(sc => sc.Competition)
                .WithMany(s => s.CompetitionsTeams)
                .HasForeignKey(sc => sc.CompetitionId);


            builder.Entity<CompetitionsTeams>()
                .HasOne<Team>(sc => sc.Team)
                .WithMany(s => s.CompetitionsTeams)
                .HasForeignKey(sc => sc.TeamId);

           builder.Entity<CompetitionsRallyRunway>().HasKey(sc => new { sc.CompetitionsId, sc.RallyRunwayId });

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=SUPER-HP\\SQLEXPRESS;Database=BgRallyRace;Trusted_Connection=True;");
            }
        }

        internal object Whery()
        {
            throw new NotImplementedException();
        }
    }
}
