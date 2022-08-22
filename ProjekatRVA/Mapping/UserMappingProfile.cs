using AutoMapper;
using ProjekatRVA.Models;
using ProjekatRVA.Models.Dto.UserDto;

namespace ProjekatRVA.Mapping
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, LoginDto>().ReverseMap();
            CreateMap<User, AuthenticationDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
