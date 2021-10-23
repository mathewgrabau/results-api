using CyclingResults.Domain;
using CyclingResults.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CyclingResults.Models.Repository
{
    public class RaceRepository : IRepository<Race>
    {
        private ApplicationDbContext _db;

        public RaceRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Race> GetAll()
        {
            return _db.Races;
        }

        public async Task<Race> Add(Race raceInstance)
        {
            if (raceInstance == null)
            {
                throw new ArgumentNullException(nameof(raceInstance));
            }

            if (raceInstance.Id != 0)
            {
                throw new ArgumentException(nameof(raceInstance));
            }

            // Finalize the instance of it.
            _db.Races.Add(raceInstance);

            int changed = await _db.SaveChangesAsync();

            throw new NotImplementedException();
        }

        public Race Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Race entityInstance)
        {
            throw new NotImplementedException();
        }
    }
}
