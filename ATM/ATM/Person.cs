using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
namespace ATM
{
    abstract class Person 
    {
        protected static ArrayList nameList;
        protected ArrayList checks;
        public Check[] ChecksOnHand 
        { get 
            {
                Check[] ret = new Check[checks.Count];
                for (int i = 0; i < ret.Length; ++i)
                    ret[i] = checks[i] as Check;
                    
                return ret; 
            } 
        }

        public void TransferCash(Cash c)
        {
            cash -= c;
        }

        protected string id;
        protected Membership memShip;
        public Membership Member { get { return memShip; } }
        public string ID { get { return id; } }
        public void CollectCash(Cash c)
        {
            cash += c;
        }
        protected Person() 
        {
            Random r = Form1.r;
            Name = (string)nameList[r.Next(nameList.Count)];
            cash = new Cash();
            memShip = new Membership(this);
            id = memShip.ID;
            checks = new ArrayList();
        }
        protected Cash cash;
        protected Type type;
        public Cash Cash { get { return cash; } protected set { cash = value; } }
        protected enum Type { CUSTOMER, WORKER };
        public string PersonType => type == Type.CUSTOMER ? "Customer" : "Worker";

        public string Name { get; protected set; }

        public static void LoadNames()
        {
            nameList = new ArrayList();

            StreamReader r = new StreamReader("..\\..\\baby-names.csv");
            while(!r.EndOfStream)
            {
                nameList.Add(r.ReadLine());
            }


            r.Close();
        }

        protected static Person Randomize()
        {
            Person p;
            Random r = Form1.r;
            if (r.Next(int.MaxValue) % 2 == 0)
                p = Customer.Random();
            else
                p = Worker.Random();

            return p;
        }


        protected void FillWallet(int total)
        {
            Random r = Form1.r;

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

    class Customer : Person
    {
        public static Person Random()
        {
            int cashTotal = 0;
            Random r = Form1.r;
            Customer c = new Customer();

            int val = r.Next() % 3;
            Wealth w = 0;

            switch (val)
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

            cashTotal = RandomAmount(r, w);
            c.FillWallet(cashTotal);

            for (int i = 0; i <= val; ++i)
            {
                c.memShip.OpenAccount(RandomAmount(r, w));
                c.checks.Add(new Check((string)nameList[r.Next(nameList.Count)], c.Name, RandomAmount(r, w)));
            }

            return c;
        }

        private static int RandomAmount(Random r, Wealth w)
        {
            int cashTotal = 0;
            switch (w)
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

            return cashTotal;
        }

        public Customer()
        {
            cash = new Cash();
            type = Type.CUSTOMER;
        }

        private enum Wealth {Lower = 128, Middle = 2048, Upper = 32768}
    }

    class Worker : Person
    {
        public static Worker Random()
        {
            Random r = Form1.r;
            Worker w = new Worker();
            int cashTotal = r.Next(1000000);
            w.FillWallet(cashTotal);

            return w;
        }

        public Worker()
        {
            type = Type.WORKER;
        }
    }
}
