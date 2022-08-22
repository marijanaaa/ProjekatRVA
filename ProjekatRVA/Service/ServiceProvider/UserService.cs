using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProjekatRVA.Models;
using ProjekatRVA.Core.IConfiguration;
using ProjekatRVA.Enums;
using ProjekatRVA.Models.Dto.UserDto;
using ProjekatRVA.Service.IServices;
using ProjekatRVA.Tokens.ITokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ProjekatRVA.Tokens.TokenProviders;

namespace ProjekatRVA.Service.ServiceProvider
{
    public class UserService : IUserService
    {
        private readonly IConfigurationSection _secretKey;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IConfiguration config, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _secretKey = config.GetSection("SecretKey");
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void EditUser(EditUserDto editUserDto)
        {
            int userId = _unitOfWork.LoggedUsers.GetUserByToken(editUserDto.Token);
            _unitOfWork.Users.EditUser(userId, editUserDto.Name, editUserDto.LastName);
            _unitOfWork.Complete();
        }

        public async Task<User> FindByUsername(string username)
        {
            return await _unitOfWork.Users.FindByUsername(username);
        }

        public List<LogDto> GetLogs(string username)
        {
            string fileName = "logs/logs" + DateTime.Now.Year.ToString() + ".txt";
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete))
            {
                StreamReader sr = new StreamReader(fs);
                string line;
                List<LogDto> logs = new List<LogDto>();
                while ((line = sr.ReadLine()) != null)
                {
                    string[] splits = line.Split(' ');
                    string user = splits[4];
                    if (username == user)
                    {
                        LogDto log = new LogDto();
                        log.Time = splits[0] + splits[1];
                        log.Type = splits[3];// err, inf
                        log.Message = splits[6];
                        logs.Add(log);
                    }
                }
                sr.Close();
                logs.Reverse();
                return logs;
            }
        }

        public UserDto GetUser(int userId)
        {
            User user = _unitOfWork.Users.GetUser(userId);
            UserDto userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        public async Task<int> GetUserId(LoginDto dto)
        {
            User user = await _unitOfWork.Users.FindByUsername(dto.UserName);
            return user != null ? user.Id : 0; 
        }

        public string GetUsernameByToken(string token)
        {
            int userId = _unitOfWork.LoggedUsers.GetUserByToken(token);
            User user = _unitOfWork.Users.GetUser(userId);
            return user.Username;
        }

        public async Task<AuthenticationDto> Login(LoginDto dto)
        {
            User user = await _unitOfWork.Users.FindByUsername(dto.UserName);
            IGenerateToken tokenGenerator = new GenerateJwtToken();
            SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey.Value));
            string tokenString = tokenGenerator.GenerateToken(user, secretKey);
            AuthenticationDto authDTO = _mapper.Map<AuthenticationDto>(user);
            authDTO.Token = tokenString;
            return authDTO;
        }

        public void Logout(TokenDto tokenDto)
        {
            int userId = _unitOfWork.LoggedUsers.GetUserByToken(tokenDto.Token);
            User user = _unitOfWork.Users.FindById(userId).Result;
            user.IsLoggedIn = false;
            _unitOfWork.Users.Update(user);
            _unitOfWork.LoggedUsers.DeleteLoggedUser(userId);
            _unitOfWork.Complete();
        }

        public void RegisterUser(RegisterDto registerDto)
        {
            User user = new User();
            user.Name = registerDto.Name;
            user.LastName = registerDto.LastName;
            user.Username = registerDto.Username;
            user.Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
            if (registerDto.UserType == "ADMIN")
            {
                user.UserType = EUserType.ADMIN;
            }
            else {
                user.UserType = EUserType.GUEST;
            }
            user.IsLoggedIn = false;
            _unitOfWork.Users.Add(user);
            _unitOfWork.Complete();
        }

        public bool UpdateLoggedIn(LoginDto dto)
        {
            User user = _unitOfWork.Users.FindByUsername(dto.UserName).Result;
            user.IsLoggedIn = true;
            bool res = _unitOfWork.Users.UpdateLoggedIn(user);
            _unitOfWork.Complete();
            return res;
        }
    }
}
