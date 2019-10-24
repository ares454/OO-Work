using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    class Dispenser
    {
        Cash cash;
        public Cash Dispense(int total)
        {
            if (total > cash.Total)
                throw new Exception();

            int twenties = total / 20, tens = total % 20 == 0 ? 0 : 1;

            if(twenties > cash.Twenties)
            {
                tens += (twenties - cash.Twenties) * 2;
                twenties = cash.Twenties;
            }
            if (tens > cash.Tens)
                throw new Exception();

            return new Cash(0, 0, twenties, tens, 0, 0);
        }

        public void TransferFromReceiver(Cash c)
        {
            cash += c;
        }
    }

    class Receiver
    {
        //Add deposit cash to account
        //Returns cash to be transferred to dispoenser
        public Cash Process(Cash c, Account a)
        {
            return new Cash(0, 0, c.Twenties, c.Tens, 0, 0);
        }

        //public void Process(Check c)
    }
}
