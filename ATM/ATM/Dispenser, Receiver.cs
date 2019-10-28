using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

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

            Cash ret = new Cash(0, 0, twenties, tens, 0, 0);
            cash -= ret;
            return ret;
        }
        public Cash RemainingCash { get { return cash; } }
        public void TransferFromReceiver(Cash c)
        {
            cash += c;
        }

        public Dispenser()
        {
            cash = new Cash(0, 0, 2500, 5000, 0, 0);
        }
    }

    class Receiver
    {
        Cash cash;
        ArrayList checks;

        public Cash RemainingCash { get { return cash; } private set { cash = value; } }

        public Receiver()
        {
            cash = new Cash();
        }

        public Cash Receive(Cash c)
        {
            cash += c;
            return Process();
        }

        //Add deposit cash to account
        //Returns cash to be transferred to dispoenser
        public Cash Process()
        {
            Cash ret = new Cash(0, 0, cash.Twenties, cash.Tens, 0, 0);
            cash -= ret;
            return ret;
        }

        //public void Process(Check c)
    }
}
