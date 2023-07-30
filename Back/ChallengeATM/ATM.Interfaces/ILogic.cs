using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Interfaces
{
    public interface ILogic<T>
    {
        T? Find(T param);

        T? Find(int id);
    }
}
