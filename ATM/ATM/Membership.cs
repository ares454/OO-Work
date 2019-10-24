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
        ArrayList accounts = new ArrayList();

        public Membership(Person p)
        {

        }

        public void openAccount(decimal balance)
        {
            accounts.Add(new Account(balance));
        }

    }
}
