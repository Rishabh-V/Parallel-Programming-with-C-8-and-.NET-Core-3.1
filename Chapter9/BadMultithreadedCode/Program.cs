using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BadMultithreadedCode
{
    class Program
    {
        static void Main(string[] args)
        {
            //// Look up data to be populated
            Dictionary<int, int> lookupData = new Dictionary<int, int>();
            Console.WriteLine("This sample demonstrates bad code written using multi-threading. We will use this sample learn to collect and analyze dumps via different tools !");
            // Dumps are collected at a point of time. To make demonstration easier, we will perform operations inside an infinite loop.
            while (true)
            {
                try
                {
                    Parallel.For(0, 1000000, new ParallelOptions() { MaxDegreeOfParallelism = Environment.ProcessorCount }, (i) =>
                             {
                                 //// Process data returned from API and add it to a collection.
                                 Thread.Sleep(20);
                                 lookupData.Add(i, GetValueFromAPI(i));
                             });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception occurred in processing data {ex.ToString()}");
                }
            }
        }

        static int GetValueFromAPI(int i)
        {
            //// Simulate delay of 1sec for fetching data from DB/API
            Thread.Sleep(1000);
            var result = new Random().Next(0, i);
            return result;             
        }
    }
}
