using CyclingResults.Domain;
using CyclingResults.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CyclingResults.Models.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IResultUploadRepository : IRepository<ResultUpload>
    {

    }

    public class ResultUploadRepository : IResultUploadRepository
    {
        private readonly ApplicationDbContext _db;

        public ResultUploadRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ResultUpload> Add(ResultUpload entityInstance)
        {
            if (entityInstance == null)
            {
                throw new ArgumentNullException(nameof(entityInstance));
            }

            // TODO validation.

            _db.ResultUploads.Add(entityInstance);
            await _db.SaveChangesAsync();

            // TODO need to coordinate with an application service for doing the uplaod to Azure to manage that part.

            return entityInstance;
        }

        public ResultUpload Get(int id)
        {
            // TODO I might need a status value somewhere here.
            return _db.ResultUploads.Find(id);
        }

        public IEnumerable<ResultUpload> GetAll()
        {
            return _db.ResultUploads;
        }

        public Task<bool> Update(ResultUpload entityInstance)
        {
            if (entityInstance == null)
            {
                throw new ArgumentNullException(nameof(entityInstance));
            }

            ResultUpload dbInstance = _db.ResultUploads.Find(entityInstance.Id);
            if (dbInstance != null)
            {
                dbInstance.Description = entityInstance.Description;
            }

            throw new NotImplementedException();
        }
    }
}
