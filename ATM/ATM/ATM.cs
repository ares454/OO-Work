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
        #region attributes
        private Dispenser dispenser;
        private Receiver receiver;
        private Printer printer;
        private int curPage;
        Membership curMem;
        Account curAcc;
        #endregion

        #region
        private static ATM atm;
        public static ATM GetInstance()
        {
            if (atm == null)
                atm = new ATM();

            return atm;
        }

        public int CurrentScreen { get { return curPage; } }
        public Account CurrentAccount { get { return curAcc; } }
        public ATM()
        {
            dispenser = new Dispenser();
            receiver = new Receiver();
            printer = new Printer();
            curPage = 0;
        }
        public void ChangeAccount()
        {
            curPage = 1;
        }

        public Membership CurrentMembership => curMem;
        #endregion
        public bool Login(string id)
        {
            curMem = MembershipList.GetInstance()[id];
            if (curMem == null)
            {
                MessageBox.Show("Invalid Identification");
                return false;
            }

            curPage = curMem.MemType == Membership.Type.CUSTOMER ? 1 : 3;

            return true;
        }

        public void Logout() 
        {
            curPage = 0;
            curMem = null;
            curAcc = null;
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

            Cash cash = dispenser.Dispense(amt);
            printer.Print(curMem, curAcc, cash, null, Printer.Configuration.PRINT_WITHDRAWL);
            Logout();

            return cash;
        }

        public void Deposit(Cash cash)
        {
            Cash p = receiver.Receive(cash);
            if(curMem.MemType == Membership.Type.CUSTOMER)
                curAcc.Deposit(cash.Total);
            dispenser.ReceiveFromReceiver(p);
            printer.Print(curMem, curAcc, cash, null, curMem.MemType == Membership.Type.CUSTOMER ? Printer.Configuration.PRINT_DEPOSIT : Printer.Configuration.PRINT_REFILL);
            Logout();

        }


        public void Deposit(Check[] checks)
        {
            foreach(Check c in checks)
            {
                int total = receiver.Receive(c);
                curAcc.Deposit(total);
            }

            printer.Print(curMem, curAcc, null, checks, Printer.Configuration.PRINT_DEPOSIT);
            Logout();
        }

        public Cash RetreiveCashDeposits()
        {
            if (curMem.MemType != Membership.Type.EMPLOYEE)
                return new Cash();

            Cash ret = receiver.ReturnCash();
            ret += dispenser.Reset();

            printer.Print(curMem, null, ret, receiver.ChecksInReceiver(), Printer.Configuration.PRINT_CLEAR);
            return ret;
        }

        public Check[] RetreiveCheckDeposits()
        {
            Logout();
            return receiver.ReturnChecks();
        }

        #region
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
           
        public void SelectAccount(int i) { curPage = 2; curAcc = curMem.GetAccount(i); }
        //Only for this example. Would not be in final implementation
        public void AddMembership(Membership m)
        {
            MembershipList.GetInstance().AddMembership(m);
        }

        //Only for this example. Would not be in final implementation
        public Check[] CheckInfo()
        {
            return receiver.ChecksInReceiver();
        }

        //This was dumb. Designer kept looking here, so cheat I had to
        internal class WithdrawButton : global::ATM.WithdrawButton
        {
        }
        #endregion
    }
}
