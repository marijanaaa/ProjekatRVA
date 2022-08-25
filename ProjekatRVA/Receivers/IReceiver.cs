using ProjekatRVA.Enums;
using ProjekatRVA.Models;
using ProjekatRVA.Models.Dto.EventDto;
using ProjekatRVA.Models.Dto.PlannerDto;
using ProjekatRVA.Models.Dto.UserDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjekatRVA.Receivers
{
    public interface IReceiver
    {
        Task<bool> CanLogin(LoginDto loginDto);
        bool Login(LoginDto loginDto);
        Task<AuthenticationDto> LoggedUser(LoginDto loginDto);
        Task<List<PlannerDto>> GetAllPlanners(string token);
        void AddNewPlanner(AddNewPlannerDto addNewPlannerDto);
        void UpdatePlanner(EditPlannerDto editPlannerDto);
        void DeletePlanner(DeleteDto deleteDto);
        Task<List<Event>> GetAllEvents(int plannerId);
        void AddEvent(AddEventDto addEventDto);
        void UpdateEvent(UpdateEventDto updateEventDto);
        void DeleteEvent(int eventId);
        UserDto GetUser(TokenDto tokenDto);
        void EditUser(EditUserDto editUserDto);
        void RegisterUser(RegisterDto registerDto);
        void Logout(TokenDto tokenDto);
        void DuplicatePlanner(DuplicateDto duplicateDto);
        List<PlannerDto> SearchPlanners(string plannerName, string token);
        List<LogDto> GetLogs(TokenDto tokenDto);
        string GetUsernameByToken(string token);
        PlannerDto GetPlannerById(int plannerId); 
    }
}
