using ProjekatRVA.Enums;
using ProjekatRVA.Models;
using ProjekatRVA.Models.Dto.UserDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjekatRVA.Service.IServices
{
    public interface IUserService
    {
        Task<AuthenticationDto> Login(LoginDto dto);//prima objekat koji se sastoji od username i password
        bool UpdateLoggedIn(LoginDto dto);
        Task<int> GetUserId(LoginDto dto);
        Task<User> FindByUsername(string username);
        UserDto GetUser(int userId);
        void EditUser(EditUserDto editUserDto);
        void RegisterUser(RegisterDto registerDto);
        void Logout(TokenDto tokenDto);
        List<LogDto> GetLogs(string username);
        string GetUsernameByToken(string token);
    }
}
