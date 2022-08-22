using ProjekatRVA.Models;
using ProjekatRVA.Models.Dto.UserDto;
using System.Threading.Tasks;

namespace ProjekatRVA.Core.IRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> FindByUsername(string username);
        User GetUser(int userId);
        void EditUser(int userId, string name, string lastname);
    }
}
