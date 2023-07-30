using ATM.Entities;
using ATM.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.LogicLayer.Interfaces
{
    public interface ICardsLogic : ILogic<Card>, IAuthentication<Login>
    {
        Card? BlockCard(Card card);
    }
}
