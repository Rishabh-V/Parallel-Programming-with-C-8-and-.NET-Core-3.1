using System;
using System.Threading.Tasks;

namespace TaskContinuations
{
    /// <summary>
    /// Demonstrates task continuations.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Task<int> t = new Task<int>(GetData, null);
            t.Start();
            t.ContinueWith(previousTaskResult => ProcessData(previousTaskResult.Result)).ContinueWith(lastTaskResult => Console.WriteLine($"Displaying the processed data as {lastTaskResult.Result}"));
            Console.ReadLine();
        }

        static int GetData(object state)
        {
            Console.WriteLine("Getting Data starts");
            //// Simulate Getting data from DB or from API from the network.
            Task.Delay(3000); 
            Console.WriteLine("Getting Data ends");
            Console.WriteLine("------------------------------------");
            return 23101506;
        }

        static int ProcessData(object state)
        { 
            Console.WriteLine("Process Data starts");
            var input = (int)state; 
            Console.WriteLine($"Received previous task result as {input}");
            //// processing of data.
            var result = 2 * input + 1;
            Console.WriteLine($"The result of data processing is {result}");
            Console.WriteLine("------------------------------------");
            return result;
        }
    }
}
