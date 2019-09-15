using System;
using System.Threading.Tasks;

namespace NoSynchronization
{
    class Program
    {
        static async Task Main(string[] args)
        {
            BankAccount bankAccount = new BankAccount(0);
            Console.WriteLine($"Initial Balance {bankAccount.ShowBalance()}");
            await bankAccount.AddMoneyToAccountAsync();
            Console.WriteLine($"Current Balance {bankAccount.ShowBalance()}, total number of transactions - {bankAccount.NumberOfTransactions}");
            Console.Read();
        }
    }
}
