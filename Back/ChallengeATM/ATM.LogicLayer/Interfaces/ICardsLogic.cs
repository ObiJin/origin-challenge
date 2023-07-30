using ATM.Entities;
using ATM.Interfaces;

namespace ATM.LogicLayer.Interfaces
{
    public interface ICardsLogic : ILogic<Card>, IAuthentication<Login>
    {
        Card? BlockCard(Card card);

        List<Card> AllCards();

        Card? Withdraw(string number, decimal amount);
    }
}
