using Microsoft.AspNetCore.Identity;
using ProjekatRVA.Enums;
using ProjekatRVA.Models;
using ProjekatRVA.Models.Dto.EventDto;
using ProjekatRVA.Models.Dto.PlannerDto;
using ProjekatRVA.Models.Dto.UserDto;
using ProjekatRVA.Service.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjekatRVA.Receivers
{
    public class Receiver : IReceiver
    {
        private readonly IUserService _userService;
        private readonly IPlannerService _plannerService;
        private readonly IEventService _eventService;
        private readonly ILoggedUsersService _loggedUsersService;
      
        public Receiver(IUserService userService, IPlannerService plannerService, IEventService eventService,  ILoggedUsersService loggedUsersService)
        {
            _userService = userService;
            _plannerService = plannerService;
            _eventService = eventService;
            _loggedUsersService = loggedUsersService;
        }
       

        public async Task<bool> CanLogin(LoginDto loginDto) 
        {
            AuthenticationDto authDto = await _userService.Login(loginDto);
            if (authDto != null)
            {
                return true;
            }
            return false;
        }

        public bool Login(LoginDto loginDto) {
            if (CanLogin(loginDto).Result)
            {
                _userService.UpdateLoggedIn(loginDto);
                return true;
            }
            return false;
        }
        public async Task<AuthenticationDto> LoggedUser(LoginDto loginDto)
        {
            AuthenticationDto authDto = await _userService.Login(loginDto);
            _loggedUsersService.InsertInLoggedUsers(authDto.Id, authDto.Token);
            return authDto;
        }

        public async Task<List<PlannerDto>> GetAllPlanners(string token)
        {
            int userId = _loggedUsersService.GetUserByToken(token);
            return await _plannerService.GetAllPlanners(userId);
        }



        public void AddNewPlanner(AddNewPlannerDto addNewPlannerDto)
        {
            int userId = _loggedUsersService.GetUserByToken(addNewPlannerDto.Token);
            _plannerService.AddNewPlanner(addNewPlannerDto, userId);
        }

        public void UpdatePlanner(EditPlannerDto editPlannerDto)
        {
            int userId = _loggedUsersService.GetUserByToken(editPlannerDto.Token);
            _plannerService.UpdatePlanner(editPlannerDto, userId);
        }

        public void DeletePlanner(DeleteDto deleteDto)
        {
            Planner planner = _plannerService.FindPlannerById(deleteDto.Id).Result;
            _plannerService.DeletePlanner(planner);
        }

        public async Task<List<Event>> GetAllEvents(int plannerId)
        {
            List<Event> retlist = new List<Event>();
            retlist = await _eventService.GetAllEvents(plannerId);
            return retlist;
        }

        public void AddEvent(AddEventDto eventDto)
        {
            _eventService.AddEvent(eventDto);
        }

        public void UpdateEvent(UpdateEventDto updateEventDto)
        {
            _eventService.UpdateEvent(updateEventDto);
        }

        public void DeleteEvent(int eventId)
        {
            _eventService.DeleteEvent(eventId);
        }

        public UserDto GetUser(TokenDto tokenDto)
        {
            int userId = _loggedUsersService.GetUserByToken(tokenDto.Token);
            UserDto userDto = _userService.GetUser(userId);
            return userDto;
        }

        public void EditUser(EditUserDto editUserDto)
        {
            _userService.EditUser(editUserDto);
        }

        public void RegisterUser(RegisterDto registerDto)
        {
            _userService.RegisterUser(registerDto);
        }

        public void Logout(TokenDto tokenDto)
        {
            _userService.Logout(tokenDto);
        }

        public void DuplicatePlanner(DuplicateDto duplicateDto)
        {
            Planner planner = _plannerService.FindPlannerById(duplicateDto.PlannerId).Result;
            int userId = _loggedUsersService.GetUserByToken(duplicateDto.Token);
            AddNewPlannerDto duplicate = new AddNewPlannerDto();
            duplicate.PlannerName = planner.PlannerName;
            duplicate.Token = duplicateDto.Token;
            _plannerService.AddNewPlanner(duplicate, userId);
            int plannerId = 0;
            List<PlannerDto> planners = _plannerService.GetAllPlanners(userId).Result;
            foreach (var item in planners)
            {
                if (item.PlannerName == duplicate.PlannerName && item.Id != duplicateDto.PlannerId) { 
                    plannerId = item.Id;
                }
            }
            List<Event> events = _eventService.GetAllEvents(duplicateDto.PlannerId).Result;
            foreach (var item in events)
            {
                AddEventDto eventDto = new AddEventDto();
                eventDto.PlannerId = plannerId;
                eventDto.DateAndTime = item.DateAndTime;
                eventDto.Text = item.Text;
                _eventService.AddEvent(eventDto);
            }
            
        }

        public List<PlannerDto> SearchPlanners(string plannerName)
        {
            return _plannerService.GetAllPlannersByName(plannerName);
        }

        public List<LogDto> GetLogs(TokenDto tokenDto)
        {
            int userId = _loggedUsersService.GetUserByToken(tokenDto.Token);
            UserDto userDto = _userService.GetUser(userId);
            return _userService.GetLogs(userDto.Username);
        }

        public string GetUsernameByToken(string token)
        {
            return _userService.GetUsernameByToken(token);
        }

        public PlannerDto GetPlannerById(int plannerId)
        {
            Planner planner = _plannerService.FindPlannerById(plannerId).Result;
            PlannerDto plannerDto = new PlannerDto();
            plannerDto.PlannerName = planner.PlannerName;
            plannerDto.Id = plannerId;
            plannerDto.Time = planner.Time;
            return plannerDto;
        }
    }
}
