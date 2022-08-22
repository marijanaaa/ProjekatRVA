using AutoMapper;
using ProjekatRVA.Core.IConfiguration;
using ProjekatRVA.Models;
using ProjekatRVA.Service.IServices;
using System.Threading.Tasks;

namespace ProjekatRVA.Service.ServiceProvider
{
    public class LoggedUsersService : ILoggedUsersService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LoggedUsersService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public int GetUserByToken(string token)
        {
            int userId = _unitOfWork.LoggedUsers.GetUserByToken(token);
            return userId;
        }

        public void InsertInLoggedUsers(int userId, string token)
        {
            _unitOfWork.LoggedUsers.InsertInLoggedUsers(userId, token);
            _unitOfWork.Complete();
        }

        public bool IsAlreadyExists(int userId)
        {
            return _unitOfWork.LoggedUsers.IsAlreadyExists(userId);
        }

        public void UpdateLoggedUser(int userId, string token)
        {
            _unitOfWork.LoggedUsers.UpdateLoggedUser(userId, token);
            _unitOfWork.Complete();
        }
    }
}
