using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    class ATM
    {
        private Dispenser dispenser;

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

    }
}
