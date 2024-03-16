using Microsoft.EntityFrameworkCore;
using WebFootballers.Models.Entities;
using WebFootballers.Models.Utility;

namespace WebFootballers.Data
{
    public class WebFootballersDbContext : DbContext 
    {
        public DbSet<Footballer> Footballers { get; set; } = null!;
        public DbSet<FootballTeam> FootballTeams { get; set; } = null!;

        public WebFootballersDbContext(DbContextOptions<WebFootballersDbContext> options) :base(options) 
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Footballer>().Property(f => f.Name).HasColumnName("FirstName");
            base.OnModelCreating(modelBuilder);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder builder)
        {
            base.ConfigureConventions(builder);

            builder.Properties<DateOnly>()
                .HaveConversion<DateOnlyConverter>();
        }
    }
}
