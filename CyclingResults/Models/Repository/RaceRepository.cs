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

            return raceInstance;
        }

        public Race Get(int id)
        {
            return _db.Races.Find(id);
        }

        public async Task<bool> Update(Race entityInstance)
        {
            if (entityInstance == null)
            {
                throw new ArgumentNullException(nameof(entityInstance));
            }

            if (entityInstance.Id == 0)
            {
                throw new ArgumentException("Expected already existing entity", nameof(entityInstance));
            }

            Race dbInstance = await _db.Races.FindAsync(entityInstance.Id);

            if (dbInstance == null)
            {
                return false;
            }

            dbInstance.Classification = entityInstance.Classification;
            dbInstance.Category = entityInstance.Category;
            dbInstance.StartTime = entityInstance.StartTime;
            dbInstance.Laps = entityInstance.Laps;
            dbInstance.Name = entityInstance.Name;
            dbInstance.RaceType = entityInstance.RaceType;

            _db.Update(dbInstance);

            int changes = await _db.SaveChangesAsync();

            return changes > 0;
        }
    }
}
