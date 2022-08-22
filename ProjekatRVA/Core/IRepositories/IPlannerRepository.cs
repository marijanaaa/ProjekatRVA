using ProjekatRVA.Models;
using ProjekatRVA.Models.Dto.UserDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjekatRVA.Core.IRepositories
{
    public interface IPlannerRepository : IGenericRepository<Planner>
    {
        Task<Planner> FindPlannerById(int id);
        Task<List<Planner>> GetAllForParticularUser(int id);
        List<Planner> GetAllPlannersByName(string plannerName);
    }
}
