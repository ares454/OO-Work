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
        public Form1()
        {
            InitializeComponent();
        }

        private void newPersonButton_Click(object sender, EventArgs e)
        {
            atMachine = Person.Randomize();

            personLabel.Text = atMachine.PersonType;
            personName.Text = atMachine.Name;
            hundredLabel.Text = atMachine.Cash.Hundreds.ToString();
            fiftyLabel.Text =   atMachine.Cash.Fifties.ToString();
            twentyLabel.Text =  atMachine.Cash.Twenties.ToString();
            tenLabrel.Text =    atMachine.Cash.Tens.ToString();
            fiveLabel.Text =    atMachine.Cash.Fives.ToString();
            oneLabel.Text =     atMachine.Cash.Ones.ToString();
            totalLabel.Text = "$" + atMachine.Cash.Total;
        }
    }
}
