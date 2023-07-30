using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Interfaces
{
    public interface IMap<T, U> where T : class
    {
        T? Map(U source);

        U? Map(T source);
    }
}
