using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ATM
{
    class Membership
    {
        string id;

        public string Owner { get; }
        public enum Type { CUSTOMER, EMPLOYEE };
        public Type MemType { get; }

        public string ID { get { return id; } }
        ArrayList accounts = new ArrayList();
        public int NumberOfAccounts { get { return accounts.Count; } }
        public Account GetAccount(int i)
        {
            if (i >= accounts.Count || i < 0)
                return null;
            return accounts[i] as Account;
        }


        public Membership(Person p)
        {
            Owner = p.Name;
            
            id = "";
            Random r = new Random();

            for(int i = 0; i < 9; i++)
                id += (char)r.Next('0', '9' + 1);

            MemType = p is Customer ? Type.CUSTOMER : Type.EMPLOYEE;
        }

        public void OpenAccount(int balance)
        {
            accounts.Add(new Account(balance));
        }

    }

}
