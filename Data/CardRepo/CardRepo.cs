using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

using basic_banking_app_server.Models.CardModel;
using basic_banking_app_server.Data.Context;

namespace basic_banking_app_server.Data.CardRepo
{
    public class CardRepo : ICardRepo
    {
        private readonly BasicBankContext _cardContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CardRepo(BasicBankContext context, IHttpContextAccessor httpContextAccessor)
        {
            _cardContext = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public bool Authorize(int userId) 
        {
            int contextUserId = (int)_httpContextAccessor.HttpContext.Items["userId"];

            if (contextUserId != userId)
            {
                return false;
            }
            return true;
        }
        public IEnumerable<Card> GetAllCards()
        {
            var cards = _cardContext.Cards.ToList();

            return cards;
        }

        public Card GetCardById(int id)
        {
            var card = _cardContext.Cards.FirstOrDefault(card => card.Id == id);

            return card;
        }

        public Card GetCardByUserId(int userId)
        {
            var card = _cardContext.Cards.FirstOrDefault(card => card.UserId == userId);

            return card;
        }

      
    }
}
