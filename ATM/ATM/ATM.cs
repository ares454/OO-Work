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
        private Receiver receiver;
        private static ATM atm;
        public static ATM GetInstance()
        {
            if (atm == null)
                atm = new ATM();

            return atm;
        }

        private int curPage;
        public int CurrentScreen { get { return curPage; } }
        public Account CurrentAccount { get { return curAcc; } }
        Membership curMem;
        Account curAcc;

        public Membership CurrentMembership => curMem;
        public bool Login(string id)
        {
            curMem = MembershipList.GetInstance()[id];
            if (curMem == null)
            {
                MessageBox.Show("Invalid Identification");
                return false;
            }

            curPage = curMem.MemType == Membership.Type.CUSTOMER ? 1 : 0;

            return true;
        }

        public void Logout() 
        {
            curPage = 0;
            curMem = null;
            curAcc = null;
        }

        public ATM()
        {
            dispenser = new Dispenser();
            receiver = new Receiver();
            curPage = 0;
        }

        public Cash Withdraw(int amt)
        {
            if(amt > curAcc.Balance)
            {
                MessageBox.Show("Account funds insufficient for withdraw amount.");
                return null;
            }
            else if(amt > dispenser.RemainingCash.Total)
            {
                MessageBox.Show("Machine has insuffient funds to release");
                return null;
            }

            curAcc.Withdraw(amt);

            return dispenser.Dispense(amt);
        }

        public void Deposit(Cash c)
        {
            Cash p = receiver.Receive(c);
            curAcc.Deposit(c.Total);
            dispenser.TransferFromReceiver(p);
        }

        public Cash CashInDispenser() { return dispenser.RemainingCash; }
        public Cash CashInReceiver() { return receiver.RemainingCash; }

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
            public void AddMembership(Membership m) {if(!memberships.ContainsKey(m.ID)) memberships.Add(m.ID, m); }
        }
           
        public void SelectAccount(int i) { curAcc = curMem.GetAccount(i); }
        //Only for this example. Would not be in final implementation
        public void AddMembership(Membership m)
        {
            MembershipList.GetInstance().AddMembership(m);
        }

        //This was dumb. Designer kept looking here, so cheat I had to
        internal class WithdrawButton : global::ATM.WithdrawButton
        {
        }
    }
}
