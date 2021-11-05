using CyclingResults.Domain;
using CyclingResults.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CyclingResults.Models.Repository
{

    public interface IEventRepository : IRepository<Event>
    {
        /// <summary>
        /// Added functionality to include the associated race objects in the return results.
        /// </summary>
        /// <param name="includeRaces"></param>
        /// <returns></returns>
        IEnumerable<Event> GetAll(bool includeRaces);

        Event Get(int id, bool includeRaces);
    }

    // TODO I need an implementation that will actually work for me.
    // That means that it can actually store the items in here.
    public class EventRepository : IEventRepository
    {
        private ApplicationDbContext _db;

        public EventRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Event> GetAll()
        {
            return _db.Events;
        }

        public IEnumerable<Event> GetAll(bool includeRaces)
        {
            if (includeRaces)
            {
                return _db.Events.Include(e => e.Races);
            }

            return GetAll();
        }

        public async Task<Event> Add(Event eventInstance)
        {
            if (eventInstance == null)
            {
                throw new ArgumentNullException(nameof(eventInstance));
            }

            _db.Events.Add(eventInstance);
            var result = await _db.SaveChangesAsync();

            if (result == 0)
            {
                return null;
            }

            return eventInstance;
        }

        // TODO make it async here?
        public Event Get(int id)
        {
            return _db.Events.Find(id);
        }

        public Event Get(int id, bool includeRaces)
        {
            if (includeRaces)
            {
                var task = _db.Events.Include(e => e.Races).FirstAsync(e => e.Id == id);
                task.Wait();
                return task.Result;
            }

            return Get(id);
        }

        public async Task<bool> Update(Event eventInstance)
        {
            if (eventInstance == null)
            {
                throw new ArgumentNullException(nameof(eventInstance));
            }

            var dbObject = Get(eventInstance.Id);
            if (dbObject == null)
            {
                throw new ArgumentException(nameof(eventInstance));
            }

            dbObject.Date = eventInstance.Date;
            dbObject.Latitude = eventInstance.Latitude;
            dbObject.Longitude = eventInstance.Longitude;
            dbObject.Name = eventInstance.Name;

            _db.Update(dbObject);
            int changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Event entityInstance)
        {
            throw new NotImplementedException();
        }
    }
}
