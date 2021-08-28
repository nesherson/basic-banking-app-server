using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

using basic_banking_app_server.Data.CardRepo;
using basic_banking_app_server.Models.CardModel;
using basic_banking_app_server.Dtos.CardDtos;

namespace basic_banking_app_server.Controllers.Users
{
    [Authorize]
    [Route("cards")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ICardRepo _cardRepo;
        private readonly IMapper _mapper;
        public CardsController(ICardRepo cardRepo, IMapper mapper)
        {
            _cardRepo = cardRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Card>> GetAllCards()
        {
            var cards = _cardRepo.GetAllCards();

            var cardsReadModel = _mapper.Map<IEnumerable<CardReadDto>>(cards);

            return Ok(cardsReadModel);
        }
    }
}
