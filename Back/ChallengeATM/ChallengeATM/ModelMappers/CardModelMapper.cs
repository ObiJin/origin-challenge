using ATM.Entities;
using ATM.Interfaces;
using ChallengeATM.Models;

namespace ChallengeATM.ModelMappers
{
    public class CardModelMapper : IMap<CardModel, Card>
    {
        public CardModel? Map(Card source)
        {
            if (source is null) { return null; }

            return new CardModel
            {
                CardNumber = source.CardNumber,
                ExpireDate = source.ExpireDate
            };
        }

        public Card? Map(CardModel source)
        {
            if (source is null) { return null; }

            return new Card
            {
                CardNumber = source.CardNumber,
                ExpireDate = source.ExpireDate
            };
        }
    }
}
