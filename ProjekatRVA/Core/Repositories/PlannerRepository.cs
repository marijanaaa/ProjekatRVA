using Microsoft.EntityFrameworkCore;
using ProjekatRVA.Models;
using ProjekatRVA.Core.IRepositories;
using ProjekatRVA.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjekatRVA.Models.Dto.UserDto;

namespace ProjekatRVA.Core.Repositories
{
    public class PlannerRepository : GenericRepository<Planner>, IPlannerRepository
    {
        public PlannerRepository(PlannerDbContext planner) : base(planner)
        {

        }

        public async Task<Planner> FindPlannerById(int id)
        {
            Planner planner = await _context.Planners.SingleOrDefaultAsync<Planner>(planner => planner.Id == id);
            return planner;
        }
        public async Task<List<Planner>> GetAllForParticularUser(int userid) {
            List<Planner> planners = new List<Planner>();
            planners = await _context.Planners.Where(planner=>planner.userId == userid).ToListAsync();
            return planners;
        }

        public List<Planner> GetAllPlannersByName(string plannerName)
        {
            List<Planner> planners = new List<Planner>();
            planners = _context.Planners.Where(x=>x.PlannerName.Contains(plannerName)).ToList();
            return planners;
        }
    }
}
