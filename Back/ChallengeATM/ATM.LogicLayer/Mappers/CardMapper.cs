using ATM.Entities;
using ATM.Interfaces;

namespace ATM.LogicLayer.Mappers
{
    public class CardMapper : IMap<Card, DataLayer.DbModel.Card>
    {
        public Card? Map(DataLayer.DbModel.Card source)
        {
            if (source == null) return null;

            return new Card
            {
                Id = source.Id,
                Balance = source.Balance,
                CardNumber = source.Number,
                ExpireDate = source.ExpireDate.ToString("dd/MM/yyyy HH:mm:ss")
            };
        }

        public DataLayer.DbModel.Card? Map(Card source)
        {
            if (source == null) return null;

            return new DataLayer.DbModel.Card
            {
                Id = source.Id,
                Balance = source.Balance,
                ExpireDate = DateTime.Parse(source.ExpireDate),
                Number = source.CardNumber
            };
        }
    }
}
