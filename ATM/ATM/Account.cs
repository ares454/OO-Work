using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    class Account
    {

        public string Number { get; }
        public decimal Balance { get; }

        public Account(decimal b)
        {
            Number = "";
            Random r = new Random();
            for (int i = 0; i < 9; ++i)
                Number += (char)r.Next('0', '9' + 1);

            Balance = b;
        }
    }
}
