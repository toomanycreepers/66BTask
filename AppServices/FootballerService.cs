using Microsoft.IdentityModel.Tokens;
using WebFootballers.Data.Repositories;
using WebFootballers.Models.DTOs;
using WebFootballers.Models.Entities;
using WebFootballers.Models.Utility;

namespace WebFootballers.AppServices
{
    public class FootballerService
    {
        private FootballerRepository _fRepo;
        private FootballTeamRepository _teamRepo;
        private bool disposed = false;

        public FootballerService(FootballerRepository footballerRepository,FootballTeamRepository teamRepository)
        {
            _fRepo = footballerRepository;
            _teamRepo = teamRepository;
        }

        public async Task<List<Footballer>> GetAllFootballers()
        {
            return await _fRepo.GetAllFootballers();
        }

        public async Task AddFootballer(FootballerCreationDTO dto)
        {
            if (Enum.TryParse(dto.Country, out Country country))
            {
                if ((await _teamRepo.FindBySearchTerm(dto.Team)).IsNullOrEmpty())
                {
                    await _teamRepo.Add(new FootballTeam() {Name = dto.Team});
                }

                var footballer = new Footballer
                {
                    Name = dto.FirstName,
                    Surname = dto.Surname,
                    DateOfBirth = DateOnly.Parse(dto.Dob),
                    IsMale = bool.Parse(dto.IsMale),
                    Country = country,
                    Team = await _teamRepo.GetByName(dto.Team)
                };
                await _fRepo.Add(footballer);
            }
            else
            {
                throw new ArgumentException("Invalid country");
            }
        }

        public async Task AlterFootballer(FootballerDTO dto)
        {
            if (Enum.TryParse(dto.Country, out Country country))
            {
                var footballer = new Footballer
                {
                    Id = dto.Id,
                    Name = dto.FirstName,
                    Surname = dto.Surname,
                    DateOfBirth = DateOnly.Parse(dto.Dob),
                    IsMale = bool.Parse(dto.IsMale),
                    Country = country,
                    Team = await _teamRepo.GetByName(dto.Team)
                };
                await _fRepo.Update(footballer);
            }
            else
            {
                throw new ArgumentException("Invalid Country");
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _fRepo.Dispose();
                    _teamRepo.Dispose();
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