using ATM.Entities;
using ATM.Interfaces;
using ATM.LogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.LogicLayer.Mappers
{
    public class CardMapper : IMap<Card, DataLayer.DbModel.Card>
    {
        public Card? Map(DataLayer.DbModel.Card source)
        {
            if (source == null) return null;

            return new Card
            {
                Balance = source.Balance,
                CardNumber = source.Number,
                ExpireDate = source.ExpireDate,
                PINNumber = source.Pin
            };
        }

        public DataLayer.DbModel.Card? Map(Card source)
        {
            if (source == null) return null;

            return new DataLayer.DbModel.Card
            {
                Pin = source.PINNumber,
                Balance = source.Balance,
                ExpireDate = source.ExpireDate,
                Number = source.CardNumber
            };
        }
    }
}
