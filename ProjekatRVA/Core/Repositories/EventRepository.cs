using Microsoft.EntityFrameworkCore;
using ProjekatRVA.Models;
using ProjekatRVA.Core.IRepositories;
using ProjekatRVA.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using ProjekatRVA.Models.Dto.EventDto;

namespace ProjekatRVA.Core.Repositories
{
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        public EventRepository(PlannerDbContext planner) : base(planner)
        {

        }

        public void AddEvent(Event _event)
        {
            _context.Events.Add(_event);
        }

        public void DeleteEvent(int eventId)
        {
            Event ev = _context.Events.Where(x =>x.Id == eventId).FirstOrDefault();
            _context.Events.Remove(ev);
        }

        public async Task<List<Event>> GetAllEvents(int plannerId)
        {
            List<Event> events = new List<Event>();
            events = await _context.Events.Where(x=>x.plannerId == plannerId).ToListAsync();
            return events;
        }

        public void UpdateEvent(UpdateEventDto updateEventDto)
        {
            Event ev = _context.Events.Where(x=>x.Id == updateEventDto.Id).FirstOrDefault();
            ev.Text = updateEventDto.Text;
            ev.DateAndTime = updateEventDto.DateAndTime;
            ev.Time = DateTime.Now;
            _context.Update(ev);
        }
    }
}
