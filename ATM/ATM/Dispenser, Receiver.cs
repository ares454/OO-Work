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

        public Cash DispenseExtra()
        {
            int twenties = cash.Twenties - 2500 > 0 ? cash.Twenties - 2500 : 0;
            int tens = cash.Tens - 5000 > 0 ? cash.Tens - 5000 : 0;

            Cash ret = new Cash(0,0,twenties, tens, 0,0) ;
            cash -= ret;
            return ret;
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
            checks = new ArrayList();
        }

        public Cash ReturnCash()
        {
            Cash ret = cash;
            cash = new Cash();
            return ret;
        }

        public Cash Receive(Cash c)
        {
            cash += c;
            return Process();
        }

        public int Receive(Check c)
        {
            checks.Add(c);
            return (int)c.Total;
        }

        public  Check[] ReturnChecks()
        {
            Check[] ret = ChecksInReceiver();

            checks.Clear();
            return ret;
        }

        public Check[] ChecksInReceiver()
        {
            Check[] ret = new Check[checks.Count];
            for (int i = 0; i < ret.Length; ++i)
                ret[i] = checks[i] as Check;
            return ret;
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
