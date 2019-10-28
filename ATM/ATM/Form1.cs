using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATM
{
    public partial class Form1 : Form
    {
        public static Random r = new Random();
        Person atMachine;
        bool changeTab = false;
        int currTab = 0;

        //Need this for screen setup. Form deals with controls, not ATM
        private Membership memShip;
        string id;
        public Form1()
        {
            InitializeComponent();
            Person.LoadNames();
            UpdateATMInfo();
        }

        private void newPersonButton_Click(object sender, EventArgs e)
        {
            atMachine = Customer.Random();
            idInput.Text = atMachine.ID;
            FillPersonInfo();
        }


        private void workerButton_Click(object sender, EventArgs e)
        {
            atMachine = Worker.Random();
            FillPersonInfo();
        }

        private void idInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (idInput.TextLength < 9 && e.KeyChar >= '0' && e.KeyChar <= '9')
            {
                string text = idInput.Text.Replace("_", "");
                text += e.KeyChar;
                int count = text.Length;

                idInput.Text = text;
            }
            else if (e.KeyChar == '\b')
                idInput.Text = idInput.Text.Remove(idInput.Text.Length - 1);

                e.Handled = true;
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            if (ATM.GetInstance().Login(idInput.Text))
            {
                ChangeTab(ATM.GetInstance().CurrentScreen);
                memShip = ATM.GetInstance().CurrentMembership;
                SetUpAccountsPage();
            }
        }

        #region EZ Detail Set Up
        private void FillPersonInfo()
        {
            personLabel.Text = atMachine.PersonType;
            personName.Text = atMachine.Name;
            idLabel.Text = atMachine.ID;
            hundredLabel.Text = atMachine.Cash.Hundreds.ToString();
            fiftyLabel.Text = atMachine.Cash.Fifties.ToString();
            twentyLabel.Text = atMachine.Cash.Twenties.ToString();
            tenLabrel.Text = atMachine.Cash.Tens.ToString();
            fiveLabel.Text = atMachine.Cash.Fives.ToString();
            oneLabel.Text = atMachine.Cash.Ones.ToString();
            totalLabel.Text = "$" + atMachine.Cash.Total;

            ATM.GetInstance().AddMembership(atMachine.Member);
            Check[] checks = atMachine.ChecksOnHand;
            personChecks.Items.Clear();


            foreach (Check c in checks)
            {
                personChecks.Items.Add(c);
            }
        }
        private void SetUpAccountsPage()
        {
            logoutButton.Visible = true;
            Account a = memShip.GetAccount(0);
            chkButton.Text = $"Checking Account *****{ a.Number.Remove(0, 5)} ${a.Balance}";

            if (memShip.NumberOfAccounts > 1)
            {
                a = memShip.GetAccount(1);
                savButton.Text = $"Savings Account *****{ a.Number.Remove(0, 5)} ${a.Balance}";
                savButton.Visible = true;
            }
            else
                savButton.Visible = false;

            if (memShip.NumberOfAccounts > 2)
            {
                a = memShip.GetAccount(2);
                invButton.Text = $"Investment Account *****{ a.Number.Remove(0, 5)} ${a.Balance}";
                invButton.Visible = true;
            }
            else
                invButton.Visible = false;
        }
        private void ChangeTab(int index)
        {
            changeTab = true;
            atmScreen.SelectedIndex = index;
        }

        private void UpdateAccountInfo()
        {
            ATM a = ATM.GetInstance();
            accountInfo.Text = $"Account# {a.CurrentAccount.Number} ${a.CurrentAccount.Balance}";
        }

        private void UpdateATMInfo()
        {
            ATM a = ATM.GetInstance();
            Cash dc = a.CashInDispenser();
            Cash rc = a.CashInReceiver();

            d20Label.Text = dc.Twenties.ToString();
            d10Label.Text = dc.Tens.ToString();
            dTotalLabel.Text = dc.Total.ToString();

            r100Label.Text = rc.Hundreds.ToString();
            r50Label.Text = rc.Fifties.ToString();
            r20Label.Text = rc.Twenties.ToString();
            r10Label.Text = rc.Tens.ToString();
            r5Label.Text = rc.Fives.ToString();
            r1Label.Text = rc.Ones.ToString();
            rTotalLabel.Text = rc.Total.ToString();
        }

        #endregion

        private void atmScreen_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (!changeTab)
                e.Cancel = true;
            else
                changeTab = false;
        }

        private void logoutButton_Click(object sender, EventArgs e)
        {
            ATM.GetInstance().Logout();
            logoutButton.Visible = false;
            changeTab = true;
            ChangeTab(ATM.GetInstance().CurrentScreen);
        }

        #region Customer Account Methods

        private void withdrawButton_Click(object sender, EventArgs e)
        {
            WithdrawButton b = sender as WithdrawButton;
            Cash c = ATM.GetInstance().Withdraw(b.Amount);
            if(c != null)
            {
                atMachine.CollectCash(c);
                FillPersonInfo();
                UpdateAccountInfo();
                UpdateATMInfo();
            }
        }

        private void chkButton_Click(object sender, EventArgs e)
        {
            ATM.GetInstance().SelectAccount(0);
            ChangeTab(2);
            UpdateAccountInfo();
        }

        private void savButton_Click(object sender, EventArgs e)
        {
            ATM.GetInstance().SelectAccount(1);
            ChangeTab(2);
            UpdateAccountInfo();
        }

        private void invButton_Click(object sender, EventArgs e)
        {
            ATM.GetInstance().SelectAccount(2);
            ChangeTab(2);
            UpdateAccountInfo();
        }

        private void custAmount_ValueChanged(object sender, EventArgs e)
        {
            wdCust.Text = $"${custAmount.Value}";
            wdCust.Amount = (int)custAmount.Value;
        }
        #endregion

        private void depCashButton_Click(object sender, EventArgs e)
        {
            ATM.GetInstance().Deposit(atMachine.Cash);
            atMachine.TransferCash(atMachine.Cash);
            UpdateATMInfo();
            UpdateAccountInfo();
            FillPersonInfo();
        }
    }
}
