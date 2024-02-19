using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebFootballers.Data;

namespace WebFootballers.AppServices
{
    public class FootballTeamService
    {
        private readonly WebFootballersDbContext _context;

        public FootballTeamService(WebFootballersDbContext context)
        {
            _context = context;
        }

        public async Task<List<string>> GetRelevantTeamNames(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                throw new ArgumentException("Search term cannot be null or empty");
            }

            var teamNames = await _context.FootballTeams
                .Where(team => team.Name.Contains(searchTerm))
                .Select(team => team.Name)
                .ToListAsync();

            return teamNames;
        }
    }
}
