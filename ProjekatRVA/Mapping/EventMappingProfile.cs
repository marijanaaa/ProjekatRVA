using AutoMapper;
using ProjekatRVA.Models;
using ProjekatRVA.Models.Dto.EventDto;

namespace ProjekatRVA.Mapping
{
    public class EventMappingProfile : Profile
    {
        public EventMappingProfile()
        {
            CreateMap<Event, AddEventDto>().ReverseMap();
        }
    }
}
