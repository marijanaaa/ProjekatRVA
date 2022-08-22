using AutoMapper;
using Microsoft.Extensions.Configuration;
using ProjekatRVA.Models;
using ProjekatRVA.Core.IConfiguration;
using ProjekatRVA.Service.IServices;
using ProjekatRVA.Tokens.ITokens;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using ProjekatRVA.Models.Dto.PlannerDto;

namespace ProjekatRVA.Service.ServiceProvider
{
    public class PlannerService : IPlannerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public PlannerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public void AddNewPlanner(AddNewPlannerDto dto, int userId)
        {
            //novi maper treba napraviti da mi vrati planner a prosledim mu addNewPlannerDto
            Planner planner = _mapper.Map<Planner>(dto);
            planner.userId = userId;
            planner.Time = DateTime.Now;
            _unitOfWork.Planners.Add(planner);
            _unitOfWork.Complete();
        }

        public void DeletePlanner(Planner planner)
        {
            _unitOfWork.Planners.Delete(planner);
            _unitOfWork.Complete();
        }

        public async Task<Planner> FindPlannerById(int id)
        {
            Planner planner = await _unitOfWork.Planners.FindPlannerById(id);
            return planner;
        }

        public async Task<List<PlannerDto>> GetAllPlanners(int userId)
        {
            List<PlannerDto> retlist = new List<PlannerDto>();
            List<Planner> planners = new List<Planner>();
            planners = await _unitOfWork.Planners.GetAllForParticularUser(userId);
            foreach (var item in planners)
            {
               PlannerDto dto = _mapper.Map<PlannerDto>(item);
               retlist.Add(dto);
            }
            return retlist;
        }

        public List<PlannerDto> GetAllPlannersByName(string plannerName)
        {
            List<Planner> planners = _unitOfWork.Planners.GetAllPlannersByName(plannerName);
            List<PlannerDto> retlist = new List<PlannerDto>();
            foreach (var item in planners)
            {
                PlannerDto plannerDto = new PlannerDto();
                plannerDto.PlannerName = item.PlannerName;
                plannerDto.Id = item.Id;
                retlist.Add(plannerDto);
            }
            return retlist;
        }

        public void UpdatePlanner(EditPlannerDto dto, int userId)
        {//bespotreban je userid
            //getplanner po id
            Planner planner = _unitOfWork.Planners.FindById(dto.Id).Result;
            planner.PlannerName = dto.PlannerName;
            planner.Time = DateTime.Now;
            _unitOfWork.Planners.Update(planner);
            _unitOfWork.Complete();
        }
    }
}
