using System;
using System.Threading.Tasks;

namespace HomeLoanVerificationSimulatorUsingBarrier
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Welcome to home loan analyzer, please enter number of sources needed for verification");
            int numberofSources = Convert.ToInt32(Console.ReadLine());
            Task[] tasks = new Task[numberofSources];
            HomeLoan homeLoan = new HomeLoan(numberofSources);
            for (int i = 0; i < numberofSources; i++)
            {
                tasks[i] = homeLoan.HomeanAnalyzerAsync(i.ToString());
            }
            await Task.WhenAll(tasks);
            if (homeLoan.HomeLoanStatus)
                Console.WriteLine("Home loan approved");
            else
                Console.WriteLine("Home loan rejected");
            Console.ReadLine();
        }

    }
}
