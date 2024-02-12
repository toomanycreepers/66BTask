using Microsoft.EntityFrameworkCore;
using WebFootballers.Models.Entities;
using WebFootballers.Models.Utility;

namespace WebFootballers.Data
{
    public class WebFootballersDbContext : DbContext 
    {
        public DbSet<Footballer> Footballers { get; set; } = null!;
        public DbSet<FootballTeam> FootballTeams { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new Configuration().GetConfiguration();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DevDB")).LogTo(s => System.Diagnostics.Debug.WriteLine(s))
                          .EnableDetailedErrors()
                          .EnableSensitiveDataLogging();
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder builder)
        {
            base.ConfigureConventions(builder);

            builder.Properties<DateOnly>()
                .HaveConversion<DateOnlyConverter>();
        }
    }
}
