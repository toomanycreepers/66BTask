using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebFootballers.Data;
using WebFootballers.Models.DTOs;
using WebFootballers.Models.Entities;
using WebFootballers.Models.Utility;

namespace WebFootballers.AppServices
{
    public class FootballerService
    {
        private readonly WebFootballersDbContext _context;

        public FootballerService(WebFootballersDbContext dbContext)
        {
            _context = dbContext;
        }

        public List<Footballer> GetAllFootballers()
        {
            var footballers = _context.Footballers.Include(f => f.Team).ToList();
            return footballers;
        }

        public void AddFootballer(FootballerCreationDTO dto)
        {
            if (Enum.TryParse(dto.Country, out Country country))
            {
                if (!(_context.FootballTeams.Any(p => p.Name.Contains(dto.Team))))
                {
                    AddTeam(dto.Team);
                }

                var footballer = new Footballer
                {
                    FirstName = dto.FirstName,
                    Surname = dto.Surname,
                    DateOfBirth = DateOnly.Parse(dto.Dob),
                    IsMale = bool.Parse(dto.IsMale),
                    Country = country,
                    Team = _context.FootballTeams.FirstOrDefault(t => t.Name == dto.Team)
                };

                _context.Add(footballer);
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Invalid country");
            }
        }

        private void AddTeam(string name)
        {
            if (string.IsNullOrEmpty(name)) return;

            var team = new FootballTeam { Name = name };
            _context.FootballTeams.Add(team);
            _context.SaveChanges();
        }

        public void AlterFootballer(FootballerDTO dto)
        {
            Footballer footballer = _context.Footballers.FirstOrDefault(f => f.Id == dto.Id);
            if (footballer == null)
            {
                throw new ArgumentException("Footballer not found");
            }

            footballer.FirstName = dto.FirstName;
            footballer.Surname = dto.Surname;
            footballer.DateOfBirth = DateOnly.Parse(dto.Dob);
            footballer.IsMale = bool.Parse(dto.IsMale);

            if (Enum.TryParse(dto.Country, out Country country))
            {
                footballer.Country = country;
            }
            else 
            {
                throw new ArgumentException("Invalid Country");
            }
            footballer.Team = _context.FootballTeams.FirstOrDefault(t => t.Name == dto.Team);
            _context.SaveChanges();
        }
    }
}