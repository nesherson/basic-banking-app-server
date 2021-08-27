using AutoMapper;

using basic_banking_app_server.Models;
using basic_banking_app_server.Dtos.UserDto;
using basic_banking_app_server.Dtos.AuthDto;

namespace basic_banking_app_server.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<User, UserReadDto>();
            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>();
            CreateMap<User, UserUpdateDto>();
            CreateMap<UserRegisterDto, User>();
            CreateMap<User, UserTokenDto>();

        }
    }
}
