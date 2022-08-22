using AutoMapper;
using ProjekatRVA.Models;
using ProjekatRVA.Core.IConfiguration;
using ProjekatRVA.Service.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using ProjekatRVA.Models.Dto.EventDto;

namespace ProjekatRVA.Service.ServiceProvider
{
    public class EventService : IEventService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EventService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void AddEvent(AddEventDto addEventDto)
        {
            Event ev = new Event();
            ev.Text = addEventDto.Text;
            ev.DateAndTime = addEventDto.DateAndTime;
            ev.plannerId = addEventDto.PlannerId;
            ev.Time = DateTime.Now;
            _unitOfWork.Events.AddEvent(ev);
            _unitOfWork.Complete();
        }

        public void DeleteEvent(int eventId)
        {
            _unitOfWork.Events.DeleteEvent(eventId);
            _unitOfWork.Complete();
        }

        public async Task<List<Event>> GetAllEvents(int plannerId)
        {
            return await _unitOfWork.Events.GetAllEvents(plannerId);
        }

      

        public void UpdateEvent(UpdateEventDto updateEventDto)
        {
            _unitOfWork.Events.UpdateEvent(updateEventDto);
            _unitOfWork.Complete();
        }
    }
}
