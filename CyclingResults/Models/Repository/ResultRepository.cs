using CyclingResults.Domain;
using CyclingResults.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CyclingResults.Models.Repository
{
    public class ResultRepository : IRepository<Result>
    {
        private ApplicationDbContext _db;

        public ResultRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Result> Add(Result entityInstance)
        {
            if (entityInstance == null)
            {
                throw new ArgumentNullException(nameof(entityInstance));
            }

            _db.Results.Add(entityInstance);
            await _db.SaveChangesAsync();

            return entityInstance;
        }

        public Result Get(int id)
        {
            return _db.Results.Find(id);
        }

        public IEnumerable<Result> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(Result entityInstance)
        {
            if (entityInstance == null)
            {
                throw new ArgumentNullException(nameof(entityInstance));
            }

            var dbInstance = await _db.Results.FindAsync(entityInstance.Id);

            if (dbInstance == null)
            {
                throw new ArgumentException("Could not find the specified entity", nameof(entityInstance));
            }

            dbInstance.FirstName = entityInstance.FirstName;
            dbInstance.LastName = entityInstance.LastName;
            dbInstance.LapsCompleted = entityInstance.LapsCompleted;
            dbInstance.LicenseVerified = entityInstance.LicenseVerified;
            dbInstance.PlateNumber = entityInstance.PlateNumber;
            dbInstance.Place = entityInstance.Place;
            dbInstance.ResultTime = entityInstance.ResultTime;
            dbInstance.Started = entityInstance.Started;
            dbInstance.TeamName = entityInstance.TeamName;

            _db.Results.Update(dbInstance);
            var changes = await _db.SaveChangesAsync();

            return changes > 0;
        }
    }
}
