using ProjekatRVA.Models;
using ProjekatRVA.Models.Dto.PlannerDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjekatRVA.Service.IServices
{
    public interface IPlannerService
    {
        void AddNewPlanner(AddNewPlannerDto dto, int userId);
        Task<List<PlannerDto>> GetAllPlanners(int userId);
        void UpdatePlanner(EditPlannerDto dto, int userId);
        void DeletePlanner(Planner planner);
        Task<Planner> FindPlannerById(int id);
        List<PlannerDto> GetAllPlannersByName(string plannerName);
    }
}
