using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Versioning;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using WebFootballers.Data;
using WebFootballers.Models;
using WebFootballers.Models.DTOs;
using WebFootballers.Models.Entities;
using WebFootballers.Models.Utility;

namespace WebFootballers.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FootballerList()
        {
            var context = new WebFootballersDbContext();
            var footballers = context.Footballers.Include(f => f.Team).ToList();
            return View(footballers);
        }

        [HttpPost]
        public IActionResult AddFootballer([FromForm] FootballerCreationDTO dto)
        {
            if (ModelState.IsValid)
            {
                if (Enum.TryParse(dto.Country, out Country country))
                {
                    using WebFootballersDbContext dbContext = new WebFootballersDbContext();
                    if (dbContext.FootballTeams.Where(p => p.Name.Contains(dto.Team)).Select(p => p.Name).ToList().IsNullOrEmpty())
                        AddTeam(dto.Team);
                    var footballer = new Footballer
                    {

                        FirstName = dto.FirstName,
                        Surname = dto.Surname,
                        DateOfBirth = DateOnly.Parse(dto.Dob),
                        IsMale = Boolean.Parse(dto.IsMale),
                        Country = country,
                        Team = dbContext.FootballTeams.FirstOrDefault(t => t.Name == dto.Team)

                    };
                    dbContext.Add(footballer);
                    dbContext.SaveChanges();
                    return Ok();
                }
            }
            return BadRequest();
        }

        [HttpPost]
        public IActionResult AlterFootballer([FromForm] FootballerDTO dto) {
            using WebFootballersDbContext dbContext = new WebFootballersDbContext();
            if (ModelState.IsValid)
            {
                Footballer footballer = dbContext.Footballers.FirstOrDefault(f => f.Id == dto.Id);
                if (footballer == null)
                {
                    return BadRequest();
                }
                footballer.FirstName = dto.FirstName;
                footballer.Surname = dto.Surname;
                footballer.DateOfBirth = DateOnly.Parse(dto.Dob);
                footballer.IsMale = Boolean.Parse(dto.IsMale);
                if (Enum.TryParse(dto.Country, out Country country))
                    footballer.Country = country;
                else
                {
                    return BadRequest();
                }
                footballer.Team = dbContext.FootballTeams.FirstOrDefault(t => t.Name == dto.Team);
                dbContext.SaveChanges();
                return RedirectToAction("FootballerList");
            }
            return BadRequest();
        }

        private void AddTeam(string name)
        {
            if (string.IsNullOrEmpty(name)) return;

            var team = new FootballTeam { Name = name };
            using WebFootballersDbContext dbContext = new WebFootballersDbContext();
            dbContext.FootballTeams.Add(team);
            dbContext.SaveChanges();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}