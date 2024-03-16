using Microsoft.EntityFrameworkCore;
using WebFootballers.Models.Entities;

namespace WebFootballers.Data.Repositories
{
    public class FootballerRepository : IDisposable
    {
        private WebFootballersDbContext _context;
        private bool disposed = false;

        public FootballerRepository(WebFootballersDbContext dbcontext)
        {
            _context = dbcontext;
        }

        public async Task<List<Footballer>> GetAllFootballers()
        {
            var footballers = await _context.Footballers.Include(f => f.Team).AsNoTracking().ToListAsync();
            return footballers;
        }

        public async Task Add(Footballer footballer)
        {
            await _context.Footballers.AddAsync(footballer);
            _context.SaveChanges();
        }

        public async Task<Footballer> GetById(long id)
        {
            return await _context.Footballers.FindAsync(id);
        }

        public async Task Update(Footballer footballer)
        {
            Footballer footballerToChange = await _context.Footballers.FirstOrDefaultAsync(f => f.Id == footballer.Id);
            if (footballerToChange != null)
            {
                footballerToChange.Name = footballer.Name;
                footballerToChange.Surname = footballer.Surname;
                footballerToChange.IsMale = footballer.IsMale;
                footballerToChange.DateOfBirth = footballer.DateOfBirth;
                footballerToChange.Country = footballer.Country;
                footballerToChange.Team = footballer.Team;
                await _context.SaveChangesAsync();
            }
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
