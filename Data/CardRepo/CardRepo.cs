using System;
using System.Collections.Generic;
using System.Linq;

using basic_banking_app_server.Models.CardModel;
using basic_banking_app_server.Data.Context;

namespace basic_banking_app_server.Data.CardRepo
{
    public class CardRepo : ICardRepo
    {
        private readonly BasicBankContext _context;

        public CardRepo(BasicBankContext context)
        {
            _context = context;
        }
        public IEnumerable<Card> GetAllCards()
        {
            var cards = _context.Cards.ToList();

            return cards;
        }

        public Card GetCardById(int id)
        {
            var card = _context.Cards.FirstOrDefault(card => card.Id == id);

            return card;
        }

        public Card GetCardByUserId(int userId)
        {
            var card = _context.Cards.FirstOrDefault(card => card.UserId == userId);

            return card;
        }

        
    }
}
