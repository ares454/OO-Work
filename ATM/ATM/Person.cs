using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    abstract class Person 
    {
        protected Person() 
        {
            cash = new Cash();
        }
        protected Cash cash;
        Type type;
        public Cash Cash { get { return cash; } protected set { cash = value; } }
        public enum Type { CUSTOMER, WORKER };
        public string PersonType => type == Type.CUSTOMER ? "Customer" : "Worker";

        public string Name { get; set; }

        public static Person Randomize()
        {
            Person p;
            Random r = new Random();
            if (r.Next(int.MaxValue) % 2 == 0)
                p = Customer.Random();
            else
                p = Customer.Random();

            return p;
        }

    }

    class Customer : Person
    {
        public static Person Random()
        {
            int cashTotal = 0;
            Random r = new Random();
            Customer c = new Customer();

            int val = r.Next() % 3;
            Wealth w = 0;

            switch(val)
            {
                case 0:
                    w = Wealth.Lower;
                    break;
                case 1: 
                    w = Wealth.Middle;
                    break;
                case 2:
                    w = Wealth.Upper;
                    break;
            }

            switch(w)
            {
                case Wealth.Lower:
                    cashTotal = r.Next((int)Wealth.Lower);
                    break;
                case Wealth.Middle:
                    cashTotal = r.Next((int)Wealth.Lower, (int)Wealth.Middle);
                    break;
                case Wealth.Upper:
                    cashTotal = r.Next((int)Wealth.Middle, (int)Wealth.Upper);
                    break;
            }

            c.FillWallet(cashTotal);

            return c;
        }

        public Customer()
        {
            cash = new Cash();
        }

        private enum Wealth {Lower = 128, Middle = 2048, Upper = 32768}

        private void FillWallet(int total)
        {
            Random r = new Random();

            int hun, fif, twe, ten, fiv;

            hun = r.Next(total / 100);
            total -= hun * 100;

            fif = r.Next(total / 50);
            total -= fif * 50;

            twe = r.Next(total / 20);
            total -= twe * 20;

            ten = r.Next(total / 10);
            total -= ten * 10;

            fiv = r.Next(total / 5);
            total -= fiv * 5;

            Cash += new Cash(hun, fif, twe, ten, fiv, total);
        }
    }

    class Worker : Person
    {
        public static Worker Random()
        {
            throw new NotImplementedException();
        }
    }
}
