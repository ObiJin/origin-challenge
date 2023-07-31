using ATM.Entities;
using ATM.Interfaces;

namespace ATM.LogicLayer.Interfaces
{
    public interface IOperationLogic :  ILogic<Operation>
    {
        Operation Create(Card card, decimal amount, DateTime date, string code);
    }
}
