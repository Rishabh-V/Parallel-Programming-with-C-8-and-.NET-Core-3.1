using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Diagnostics.Runtime;

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
        bool isAcquired = false;

        public BankAccount(long initialAccountBalance)
        {
            this.accountBalance = initialAccountBalance;
            numberOfTransactions = 0;
        }

        /// <summary>
        /// Add money to account through multiple transactions
        /// </summary>        
        public async Task AddMoneyToAccountAsync()
        {
            var tasks = new Task[50];
             for (int i = 1; i <= tasks.Length; i++)
                {
                    tasks[i - 1] = AddBalanceToAcccount(i);
                }

            //Thread.Sleep(5000);
            //for (int i = 1; i <= tasks.Length; i++)
            //{                
            //    Console.WriteLine(tasks[i - 1].Status);
            //}
           await Task.WhenAll(tasks);
        }

        public long ShowBalance()
        {
            return this.accountBalance;
        }

        #region Private methods
        async Task AddBalanceToAcccount(long amount)
        {
            await Task.Delay(1);
            //long modifiedamount = 0;
            //lock (locker)
            //{
            //    //Thread.Sleep(2000);
            //    modifiedamount = amount / (amount - 20);
            //    accountBalance = accountBalance + amount;
            //    numberOfTransactions = numberOfTransactions + 1;
            //}

            bool lockAcquired = false;
            try
            {
                Monitor.Enter(locker, ref lockAcquired);
                //modifiedamount = amount / (amount - 20);
                accountBalance = accountBalance + amount;
                numberOfTransactions = numberOfTransactions + 1;
            }
            finally
            {
                if (lockAcquired)
                {
                    Monitor.Exit(locker);
                }
            }

        }

        void AvailableThreads()
        {
            int worker, io;
            ThreadPool.GetAvailableThreads(out worker, out io);
            Console.WriteLine("   Worker threads: {0:N0}", worker);
        }
        #endregion

    }
}
