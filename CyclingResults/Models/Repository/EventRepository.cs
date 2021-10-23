using CyclingResults.Domain;
using CyclingResults.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CyclingResults.Models.Repository
{

    // TODO I need an implementation that will actually work for me.
    // That means that it can actually store the items in here.
    public class EventRepository : IRepository<Event>
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
    }
}
