using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    class Cash
    {
        public Cash()
        {
            Hundreds = Fifties = Twenties = Tens = Fives = Ones = 0;
        }

        public Cash(int huns, int fifs, int twes, int tens, int fivs, int ones)
        {
            Hundreds = huns;
            Fifties = fifs;
            Twenties = twes;
            this.Tens = tens;
            Fives = fivs;
            this.Ones = ones;
        }

        public int Total { get { return Hundreds * 100 + Fifties * 50 + Twenties * 20 + Tens * 10 + Fives * 5 + Ones; } }
        public int Hundreds { get; }
        public int Fifties { get; }
        public int Twenties { get; }
        public int Tens { get; }
        public int Fives { get; }
        public int Ones { get; }
        public static Cash operator +(Cash c1, Cash c2)
        {
            int h, f50, t20, t10, f5, o;

            h = c1.Hundreds + c2.Hundreds;
            f50 = c1.Fifties + c2.Fifties;
            t20 = c1.Twenties + c2.Twenties;
            t10 = c1.Tens + c2.Tens;
            f5 = c1.Fives + c2.Fives;
            o = c1.Ones + c2.Ones;

            return new Cash(h, f50, t20, t10, f5, o);
        }
        public static Cash operator -(Cash c1, Cash c2)
        {
            int h, f50, t20, t10, f5, o;

            h = c1.Hundreds - c2.Hundreds;
            f50 = c1.Fifties - c2.Fifties;
            t20 = c1.Twenties - c2.Twenties;
            t10 = c1.Tens - c2.Tens;
            f5 = c1.Fives - c2.Fives;
            o = c1.Ones - c2.Ones;

            return new Cash(h, f50, t20, t10, f5, o);
        }
    }

    class Check
    {
        decimal total;
        string name;
        string payTo;
        public decimal Total { get { return total; } }
        public string Name { get { return name; } }
        public string PayTo { get { return payTo; } }

        public Check(string name)
        {

        }
    }
}
