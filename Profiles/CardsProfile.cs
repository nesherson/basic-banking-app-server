using AutoMapper;

using basic_banking_app_server.Models.CardModel;
using basic_banking_app_server.Dtos.CardDtos;


namespace basic_banking_app_server.Profiles
{
    public class CardsProfile : Profile
    {
        public CardsProfile()
        {
            CreateMap<Card, CardReadDto>();
        }
    }
}
