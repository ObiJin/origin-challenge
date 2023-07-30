using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Interfaces
{
    public interface IAuthentication<T>
    {
        T? Authenticate(string username, string password);
    }
}
