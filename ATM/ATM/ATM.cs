using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;

namespace ATM
{
    class ATM
    {
        private Dispenser dispenser;
        private static ATM atm;
        public static ATM GetInstance()
        {
            if (atm == null)
                atm = new ATM();

            return atm;
        }

        Membership curMem;
        Account curAcc;
        public bool Login(string id)
        {
            curMem = MembershipList.GetInstance()[id];
            if (curMem == null)
            {
                MessageBox.Show("Invalid Identification");
                return false;
            }

            

            return true;
        }

        public ATM()
        {
            dispenser = new Dispenser();

        }

        public Cash Dispense(int total)
        {
            try
            {
                return dispenser.Dispense(total);
            }
            catch (Exception)
            {
                
            }

            return null;
        }

        private class MembershipList
        {
            Dictionary<string, Membership> memberships;
            static private MembershipList ml = null;

            private MembershipList()
            {
                memberships = new Dictionary<string, Membership>();
            }

            static public MembershipList GetInstance()
            {
                if(ml == null)
                    ml = new MembershipList();
                return ml;
            }

            public Membership this[string i] 
            { 
                get 
                {
                    if (!memberships.ContainsKey(i))
                        return null;
                    return memberships[i];
                }
            }
            public void AddMembership(Membership m) { memberships.Add(m.ID, m); }
        }
           

        //Only for this example. Would not be in final implementation
        public void AddMembership(Membership m)
        {
            MembershipList.GetInstance().AddMembership(m);
        }
    }
}
