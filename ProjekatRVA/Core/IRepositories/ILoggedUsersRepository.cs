using ProjekatRVA.Models;
using System.Threading.Tasks;

namespace ProjekatRVA.Core.IRepositories
{
    public interface ILoggedUsersRepository : IGenericRepository<LoggedInUsers>
    {
        void InsertInLoggedUsers(int userId, string token);
        int GetUserByToken(string token);
        bool IsAlreadyExists(int userId);
        void UpdateLoggedUser(int userId, string token);
        void DeleteLoggedUser(int userId);
    }
}
