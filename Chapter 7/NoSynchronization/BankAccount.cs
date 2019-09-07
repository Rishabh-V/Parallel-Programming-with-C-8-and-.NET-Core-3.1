using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoSynchronization
{
    public class BankAccount
    {
        private long accountBalance;
        private int numberOfTransactions;

        public int NumberOfTransactions
        {
            get
            {
                return numberOfTransactions;
            }
        }

        //Lock
        object locker = new object();

        public BankAccount(long initialAccountBalance)
        {
            this.accountBalance = initialAccountBalance;
            numberOfTransactions = 0;
        }

        /// <summary>
        /// Add money to account through multiple transactions
        /// </summary>        
        public async Task AddMoneyToAccount()
        {
            var tasks = new Task[50];

            for (int i = 1; i <= tasks.Length; i++)
            {
                tasks[i - 1] = AddBalanceToAcccount(i);
            }

            await Task.WhenAll(tasks);
        }

        async Task AddBalanceToAcccount(long amount)
        {
            await Task.Delay(1);
            lock (locker)
            {
                accountBalance = accountBalance + amount;
                numberOfTransactions = numberOfTransactions + 1;
            }
        }

        public long ShowBalance()
        {
            return this.accountBalance;
        }
    }
}
