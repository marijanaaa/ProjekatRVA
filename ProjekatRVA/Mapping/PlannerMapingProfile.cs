using AutoMapper;
using ProjekatRVA.Models;
using ProjekatRVA.Models.Dto.PlannerDto;

namespace ProjekatRVA.Mapping
{
    public class PlannerMapingProfile : Profile
    {
        public PlannerMapingProfile()
        {
            CreateMap<Planner, AddNewPlannerDto>().ReverseMap();
            CreateMap<Planner, PlannerDto>().ReverseMap();
            CreateMap<Planner, EditPlannerDto>().ReverseMap();
        }
    }
}
