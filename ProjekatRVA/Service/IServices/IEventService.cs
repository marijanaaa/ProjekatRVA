using ProjekatRVA.Models;
using ProjekatRVA.Models.Dto.EventDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjekatRVA.Service.IServices
{
    public interface IEventService
    {
        Task<List<Event>> GetAllEvents(int plannerId);
        void UpdateEvent(UpdateEventDto updateEventDto);
        void DeleteEvent(int eventId);
        void AddEvent(AddEventDto addEventDto);
    }
}
