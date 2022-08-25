using ProjekatRVA.Core.IRepositories;
using ProjekatRVA.Data;
using ProjekatRVA.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ProjekatRVA.Core.Repositories
{
    public class LoggedUsersRepository : GenericRepository<LoggedInUsers>, ILoggedUsersRepository
    {
        public LoggedUsersRepository(PlannerDbContext planner) : base(planner)
        {

        }

        public void DeleteLoggedUser(int userId)
        {
            LoggedInUsers loggedInUsers = _context.LoggedInUsers.FirstOrDefault(x => x.UserId == userId);
            _context.LoggedInUsers.Remove(loggedInUsers);
        }

        public int GetUserByToken(string token)
        {
            int userId = _context.LoggedInUsers.Where(x=>x.Token == token).FirstOrDefault().UserId;
            return userId;
        }

        public void InsertInLoggedUsers(int userId, string token)
        {
            LoggedInUsers loggedInUsers = new LoggedInUsers();
            loggedInUsers.UserId = userId;
            loggedInUsers.Token = token;
            _context.LoggedInUsers.Add(loggedInUsers);
        }

        public bool IsAlreadyExists(int userId)
        {
            LoggedInUsers user = _context.LoggedInUsers.FirstOrDefault(x=>x.UserId == userId);
            if (user != null) {
                return true;
            }
            return false;
        }

        public void UpdateLoggedUser(int userId, string token)
        {
            LoggedInUsers user = _context.LoggedInUsers.FirstOrDefault(x => x.UserId == userId);
            if (user != null) { 
                user.Token = token;
                _context.LoggedInUsers.Update(user);
            }

        }
    }
}
