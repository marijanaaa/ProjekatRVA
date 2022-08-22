using ProjekatRVA.Models;
using ProjekatRVA.Models.Dto.EventDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjekatRVA.Core.IRepositories
{
    public interface IEventRepository
    {
        Task<List<Event>> GetAllEvents(int plannerId);
        void UpdateEvent(UpdateEventDto updateEventDto);
        void DeleteEvent(int eventId);
        void AddEvent(Event _event);
    }
}
