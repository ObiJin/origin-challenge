using ATM.Entities;
using ATM.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.LogicLayer.Interfaces
{
    public interface IOperationLogic :  ILogic<Operation>
    {
        Operation Create(Card card, decimal amount, DateTime date, string code);
    }
}
