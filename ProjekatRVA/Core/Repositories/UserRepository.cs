using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjekatRVA.Models;
using ProjekatRVA.Core.IRepositories;
using ProjekatRVA.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjekatRVA.Models.Dto.UserDto;

namespace ProjekatRVA.Core.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(PlannerDbContext planner) : base (planner)
        {

        }

        public void EditUser(int userId, string name, string lastname)
        {
            User user = _context.Users.FirstOrDefault(x=>x.Id == userId);
            user.Name = name;
            user.LastName = lastname;
            _context.Users.Update(user);
        }

        public async Task<User> FindByUsername(string username)
        {
            User user = await _context.Users.SingleOrDefaultAsync<User>(u => String.Equals(u.Username, username));
            return user;
        }

        public User GetUser(int userId)
        {
            User user = _context.Users.SingleOrDefault(x=>x.Id == userId);
            return user;
        }
    }
}
