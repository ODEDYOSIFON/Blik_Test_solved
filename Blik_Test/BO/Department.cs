using System;
using System.Collections.Generic;
using System.Linq;
//using System.Runtime.CompilerServices;// option 2
using System.Text;
using System.Threading.Tasks;

namespace Blik_Test.BO
{
    public class Department
    {
        int _salary;
        readonly Random r = new();
        public Department(int initial)
        {
            _salary = initial;
        }

        private Object _withdrawLock = new object(); // option 1
        // [MethodImpl(MethodImplOptions.Synchronized)] // option 2
        int Withdraw(int amount)
        {
            if (_salary < 0)
            {
                throw new Exception("Negative Balance");
            }
            lock (_withdrawLock)
            { // option 1
                if (_salary >= amount)
                {
                    Console.WriteLine("salary before Withdrawal :  " + _salary);
                    Console.WriteLine("Amount to Withdraw        : -" + amount);
                    _salary -= amount;
                    Console.WriteLine("salary after Withdrawal  :  " + _salary);
                    return amount;
                }
                else
                {
                    return 0; // transaction rejected  
                }
            }

        }
        public void DoTransactions()
        {
            for (int i = 0; i < 100; i++)
            {
                Withdraw(r.Next(1, 100));
            }
        }
    }
}
