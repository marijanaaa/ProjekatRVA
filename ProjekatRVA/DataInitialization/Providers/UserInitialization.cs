using ProjekatRVA.Models;
using ProjekatRVA.Core.IConfiguration;
using ProjekatRVA.DataInitialization.Interfaces;
using ProjekatRVA.Enums;
using System.Collections.Generic;

namespace ProjekatRVA.DataInitialization.Providers
{
    public class UserInitialization : IUserInitialization
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserInitialization(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        void IUserInitialization.UserInitialization()
        {
            if (_unitOfWork.Users.GetAll().Count == 0)
            {
                User user = new User();
                user.Name = "Marijana";
                user.LastName = "Stojanovic";
                user.Username = "admin";
                user.Password = BCrypt.Net.BCrypt.HashPassword("admin");
                user.UserType = EUserType.ADMIN;
                _unitOfWork.Users.Add(user);
                _unitOfWork.Complete();

                User user2 = new User();
                user2.Name = "Jelena";
                user2.LastName = "Stojanovic";
                user2.Username = "jelena";
                user2.Password = BCrypt.Net.BCrypt.HashPassword("jelena");
                user2.UserType = EUserType.ADMIN;
                _unitOfWork.Users.Add(user2);
                _unitOfWork.Complete();

                User user3 = new User();
                user3.Name = "Milada";
                user3.LastName = "Stojanovic";
                user3.Username = "milada";
                user3.Password = BCrypt.Net.BCrypt.HashPassword("milada");
                user3.UserType = EUserType.GUEST;
                _unitOfWork.Users.Add(user3);
                _unitOfWork.Complete();
            }
            else {
                return;
            }
           
        }
    }
}
