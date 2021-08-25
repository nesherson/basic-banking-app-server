using AutoMapper;

using basic_banking_app_server.Models;
using basic_banking_app_server.Dtos;

namespace basic_banking_app_server.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<User, UserReadDto>();
        }
    }
}
