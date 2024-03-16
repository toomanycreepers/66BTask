using Microsoft.EntityFrameworkCore;
using WebFootballers.Models.Entities;

namespace WebFootballers.Data.Repositories
{
    public class FootballTeamRepository : IDisposable
    {
        private readonly WebFootballersDbContext _context;
        private bool disposed = false;

        public FootballTeamRepository(WebFootballersDbContext dbcontext)
        {
            _context = dbcontext;
        }

        public async Task<FootballTeam> GetById(long id)
        {
            return await _context.FootballTeams.FindAsync(id);
        }

        public Task<FootballTeam> GetByName(string name)
        {
            return _context.FootballTeams.FirstOrDefaultAsync(t => t.Name.Equals(name));
        }

        public async Task<List<string>> FindBySearchTerm(string term)
        {
            return await _context.FootballTeams
                .Where(team => team.Name.Contains(term))
                .Select(team => team.Name)
                .ToListAsync();
        }


        public async Task Add(FootballTeam team)
        {
            await _context.FootballTeams.AddAsync(team);
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
