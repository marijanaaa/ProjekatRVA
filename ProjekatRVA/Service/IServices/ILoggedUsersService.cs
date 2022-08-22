using ProjekatRVA.Models;
using System.Threading.Tasks;

namespace ProjekatRVA.Service.IServices
{
    public interface ILoggedUsersService
    {
        void InsertInLoggedUsers(int userId, string token);
        int GetUserByToken(string token);
        bool IsAlreadyExists(int userId);
        void UpdateLoggedUser(int userId, string token);
    }
}
