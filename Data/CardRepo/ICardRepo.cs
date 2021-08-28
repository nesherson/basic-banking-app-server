using System.Collections.Generic;

using basic_banking_app_server.Models.CardModel;

namespace basic_banking_app_server.Data.CardRepo
{
    public interface ICardRepo
    {
        IEnumerable<Card> GetAllCards();
        Card GetCardById(int id);
    }
}
