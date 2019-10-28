using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    class Printer
    {
        public enum Configuration { PRINT_DEPOSIT, PRINT_WITHDRAWL, PRINT_REFILL, PRINT_CLEAR }
        private TransactionReport PrepareReport (Membership mem, Account acc, Cash c, Configuration configuration)
        {
            TransactionReport rep = null;
            if (configuration == Configuration.PRINT_DEPOSIT)
                rep = new CashDepositTransactionReport(mem.ID, acc, c);
            else if (configuration == Configuration.PRINT_WITHDRAWL)
                rep = new WithdrawTransactionReport(mem.ID, acc, c);

            return rep;
        }

        private TransactionReport PrepareReport (Membership mem, Account acc, Check[]checks, Configuration configuration)
        {
            return new CheckDepositTransactionReport(mem.ID, acc, checks);
        }

        private TransactionReport PrepareReport(Membership mem, Cash cash, Check[] checks, Configuration configuration)
        {
            TransactionReport rep = null;

            if (configuration == Configuration.PRINT_CLEAR)
                rep = new MachineDepositTransactionReport(mem.ID, checks, cash);
            else if (configuration == Configuration.PRINT_REFILL)
                rep = new MachineRefillTransactionReport(mem.ID, cash);

            return rep;
        }

        public void Print(Membership mem, Account acc, Cash cash, Check[] checks, Configuration configuration)
        {
            TransactionReport rep = null;

            if(acc != null)
            {
                if (cash != null)
                    rep = PrepareReport(mem, acc, cash, configuration);
                else
                    rep = PrepareReport(mem, acc, checks, configuration);
            }
            else
                rep = PrepareReport(mem, cash, checks, configuration);

            string[] lines = rep.Report();
            ReceiptForm rf = new ReceiptForm();
            rf.Show(lines);
        }
    }

}
