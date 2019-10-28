using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ATM
{
    abstract class TransactionReport
    {
        protected string membershipID;

        readonly protected string sectionList = "--------------------";
        private TransactionReport() { }
        public TransactionReport(string memID)
        {
            membershipID = memID;
        }

        public string[] ToArray(ArrayList list)
        {
            string[] lines = new string[list.Count];
            for (int i = 0; i < list.Count; ++i)
                lines[i] = ((string)list[i] + '\n');

            return lines;
        }

        abstract public string[] Report();
    }

    class CheckDepositTransactionReport : TransactionReport
    {
        Check[] checks;
        string accountNumber;
        int accountBalance;

        public CheckDepositTransactionReport(string memID, Account account, Check[] checks) :base(memID)
        {
            this.checks = checks;
            accountNumber = account.Number;
            accountBalance = (int)account.Balance;
        }

        public override string[] Report()
        {
            ArrayList list = new ArrayList();
            list.Add("Account Deposit");
            list.Add($"Member ID: *****{membershipID.Remove(0, 5)}");
            list.Add($"Account Number: *****{accountNumber.Remove(0, 5)}");
            list.Add(sectionList);

            int total = 0;
            foreach (Check c in checks)
            {
                total += (int)c.Total;
                list.Add($"\tCheck from {c.Name} to {c.PayTo}: \t${c.Total}");
            }
            list.Add($"Check Total: ${total}");
            list.Add(sectionList);

            list.Add($"Balance: ${accountBalance}");

            return ToArray(list);
        }
    }

    class CashDepositTransactionReport : TransactionReport
    {
        Cash cash;
        string accountNumber;
        int accountBalance;

        public CashDepositTransactionReport(string memID, Account account, Cash cash) : base(memID)
        {
            this.cash = cash;
            accountNumber = account.Number;
            accountBalance = (int)account.Balance;
        }

        public override string[] Report()
        {
            ArrayList list = new ArrayList();
            list.Add("Account Deposit");
            list.Add($"Member ID: *****{membershipID.Remove(0, 5)}");
            list.Add($"Account Number: *****{accountNumber.Remove(0, 5)}");
            list.Add(sectionList);

            if (cash.Hundreds > 0)  list.Add($"\t$100: \t${cash.Hundreds * 100}");
            if (cash.Fifties > 0)   list.Add($"\t$50: \t${cash.Fifties * 50}");
            if (cash.Twenties > 0)  list.Add($"\t$20: \t${cash.Twenties * 20}");
            if (cash.Tens > 0)      list.Add($"\t$10: \t${cash.Tens * 10}");
            if (cash.Fives > 0)     list.Add($"\t$5: \t${cash.Fives * 5}");
            if (cash.Ones > 0)      list.Add($"\t$1: \t${cash.Ones * 1}");
            list.Add($"\tCash Total: \t${cash.Total}");
            list.Add(sectionList);

            list.Add($"Balance: ${accountBalance}");

            return ToArray(list);
        }
    }

    class WithdrawTransactionReport : TransactionReport
    {
        Cash cash;
        string accountNumber;
        int accountBalance;
        public WithdrawTransactionReport(string memID, Account account, Cash cash) : base(memID)
        {
            this.cash = cash;
            accountNumber = account.Number;
            accountBalance = (int)account.Balance;
        }

        public override string[] Report()
        {
            ArrayList list = new ArrayList();
            list.Add("Account Deposit");
            list.Add($"Member ID: *****{membershipID.Remove(0, 5)}");
            list.Add($"Account Number: *****{accountNumber.Remove(0, 5)}");
            list.Add(sectionList);

            if (cash.Twenties > 0) list.Add($"\t$20: \t${cash.Twenties * 20}");
            if (cash.Tens > 0) list.Add($"\t$10: \t${cash.Tens * 10}");
            list.Add($"\tCash Total: \t${cash.Total}");

            list.Add($"Balance: ${accountBalance}");

            return ToArray(list);
        }
    }

    class MachineRefillTransactionReport : TransactionReport
    {
        Cash cash;
        public MachineRefillTransactionReport(string memID, Cash cash) :base(memID)
        {
            this.cash = cash;
            membershipID = memID;
        }
        public override string[] Report()
        {
            ArrayList list = new ArrayList();
            list.Add("Account Deposit");
            list.Add($"Member ID: *****{membershipID.Remove(0, 5)}");
            list.Add(sectionList);

            if (cash.Twenties > 0) list.Add($"\t$20: \t${cash.Twenties * 20}");
            if (cash.Tens > 0) list.Add($"\t$10: \t${cash.Tens * 10}");
            list.Add($"\tCash Total: \t${cash.Total}");


            return ToArray(list);
        }
    }

    class MachineDepositTransactionReport : TransactionReport
    {
        Cash cash;
        Check[] checks;
        public MachineDepositTransactionReport(string memID, Check[] checks, Cash cash) : base(memID)
        {
            this.cash = cash;
            this.checks = checks;
        }
        public override string[] Report()
        {
            ArrayList list = new ArrayList();
            list.Add("Account Deposit");
            list.Add($"Member ID: *****{membershipID.Remove(0, 5)}");
            list.Add(sectionList);

            if (cash.Hundreds > 0) list.Add($"\t$100: \t${cash.Hundreds * 100}");
            if (cash.Fifties > 0) list.Add($"\t$50: \t${cash.Fifties * 50}");
            if (cash.Twenties > 0) list.Add($"\t$20: \t${cash.Twenties * 20}");
            if (cash.Tens > 0) list.Add($"\t$10: \t${cash.Tens * 10}");
            if (cash.Fives > 0) list.Add($"\t$5: \t${cash.Fives * 5}");
            if (cash.Ones > 0) list.Add($"\t$1: \t${cash.Ones * 1}");
            list.Add($"\tCash Total: \t${cash.Total}");
            list.Add(sectionList);

            int total = 0;
            foreach (Check c in checks)
            {
                total += (int)c.Total;
                list.Add($"\tCheck from {c.Name} to {c.PayTo}: ${c.Total}");
            }
            list.Add($"Check Total: ${total}");
            list.Add(sectionList);

            list.Add($"Total Collected: ${cash.Total + total}");

            return ToArray(list);
        }
    }
}
