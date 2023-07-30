using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Entities
{
    public class Card
    {
        public string CardNumber {get;set;}
        public string PINNumber { get;set;}
        public decimal Balance { get;set;}
        public DateTime ExpireDate { get;set;}
    }
}
