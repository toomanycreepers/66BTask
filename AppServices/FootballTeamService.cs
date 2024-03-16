using Microsoft.EntityFrameworkCore;
using WebFootballers.Data.Repositories;

namespace WebFootballers.AppServices
{
    public class FootballTeamService : IDisposable
    {
        private readonly FootballTeamRepository _repo;
        private bool disposed = false;

        public FootballTeamService(FootballTeamRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<string>> GetRelevantTeamNames(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                throw new ArgumentException("Search term cannot be null or empty");
            }
            return await _repo.FindBySearchTerm(searchTerm);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _repo.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}