using System;
using System.Threading;

namespace ThreadPoolStats
{
    /// <summary>
    /// Demonstrates the Thread Pool statistics
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            DisplayThreadStats();   
            Console.ReadLine();
        }

        static void DisplayThreadStats()
        {
            int workerThreads;
            int iocompletionThreads;

            ThreadPool.GetMinThreads(out workerThreads, out iocompletionThreads);
            Console.Write($"Minimum worker threads are {workerThreads}.{Environment.NewLine}Minimum I/O Completion threads are {iocompletionThreads}.{Environment.NewLine}Processor count is {Environment.ProcessorCount}{Environment.NewLine}");
            Console.WriteLine();
            ThreadPool.GetMaxThreads(out workerThreads, out iocompletionThreads);
            Console.Write($"Maximum worker threads are {workerThreads}.{Environment.NewLine}Maximum I/O Completion threads are {iocompletionThreads}.{Environment.NewLine}");
            Console.WriteLine();
            ThreadPool.GetAvailableThreads(out workerThreads, out iocompletionThreads);
            Console.Write($"Available worker threads are {workerThreads}.{Environment.NewLine}Available I/O Completion threads are {iocompletionThreads}.");
        }
    }
}
