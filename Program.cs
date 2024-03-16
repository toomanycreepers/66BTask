using Microsoft.EntityFrameworkCore;
using WebFootballers.AppServices;
using WebFootballers.Data;
using WebFootballers.Data.Repositories;

namespace WebFootballers
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
            var dbName = Environment.GetEnvironmentVariable("DB_NAME");
            var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
            builder.Services.AddDbContext<WebFootballersDbContext>(
                options => options.UseSqlServer($"Server={dbHost};Database={dbName};User=sa;Password={dbPassword};Trust Server Certificate=True"));
            builder.Services.AddScoped<FootballerService>();
            builder.Services.AddScoped<FootballTeamService>();
            builder.Services.AddScoped<FootballerRepository>();
            builder.Services.AddScoped<FootballTeamRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<WebFootballersDbContext>();
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }
            }
            app.Run();
        }
    }
}