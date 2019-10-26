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
        Person atMachine;
        Membership memShip;
        string id;
        public Form1()
        {
            InitializeComponent();
            Person.LoadNames();
        }

        private void newPersonButton_Click(object sender, EventArgs e)
        {
            atMachine = Customer.Random();

            FillPersonInfo();
        }

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
            ATM.GetInstance().Login(idInput.Text);

            MessageBox.Show("Login Successful");
        }
    }
}
