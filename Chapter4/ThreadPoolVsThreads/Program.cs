using System;
using System.Diagnostics;
using System.Threading;

namespace ThreadPoolVsThreads
{
    /// <summary>
    /// Demonstrates ThreadPool Vs Thread performance
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            ////Thread
            Stopwatch watch = Stopwatch.StartNew();
            for (int i = 0; i < 10; i++)
            {
                var thread = new Thread(WriteToConsole);
                thread.Start($"Thread {i}");
            }

            Console.WriteLine($"Executing {nameof(WriteToConsole)} method on 10 manually created threads took {watch.ElapsedMilliseconds} milliseconds");

            watch.Restart();
            //// Thread Pool
            for (int i = 0; i < 10; i++)
            {
                ThreadPool.QueueUserWorkItem(WriteToConsole, $"ThreadPool Thread {i}");
            }

            Console.WriteLine($"Executing {nameof(WriteToConsole)} method on ThreadPool threads took {watch.ElapsedMilliseconds} milliseconds");
            Console.ReadLine();
        }

        static void WriteToConsole(object state)
        {
            string name = string.IsNullOrWhiteSpace(Thread.CurrentThread.Name) ? state.ToString() : Thread.CurrentThread.Name;
            if (string.IsNullOrWhiteSpace(Thread.CurrentThread.Name))
            {
                Thread.CurrentThread.Name = name;
            }

            Console.WriteLine($"Hello Threading World! from {Thread.CurrentThread.Name}");  //// Thread name from which message is printed.
            Console.WriteLine($"Managed Thread Id : {Thread.CurrentThread.ManagedThreadId}"); //// The managed thread identifier.
            Console.WriteLine($"IsAlive :{Thread.CurrentThread.IsAlive}"); //// Display if thread is alive.
            Console.WriteLine($"Priority :{Thread.CurrentThread.Priority}");  //// Displays the thread priority. Default is Normal.
            Console.WriteLine($"IsBackground :{Thread.CurrentThread.IsBackground}");  //// Displays if thread is Foreground or Background. By default all threads are foreground.
            Console.WriteLine($"Name :" + Thread.CurrentThread.Name);  //// Name of thread.
            Console.WriteLine($"Apartment State :{Thread.CurrentThread.ApartmentState.ToString()}"); //// This is obsolete and is shown for the sake of completeness. The apartment state.
            Console.WriteLine($"IsThreadPoolThread :{Thread.CurrentThread.IsThreadPoolThread}"); //// Displays if thread is a CLR thread pool thread
            Console.WriteLine($"ThreadState :{Thread.CurrentThread.ThreadState}");  //// Displays the thread state
            Console.WriteLine($"Current Culture : {Thread.CurrentThread.CurrentCulture}"); //// Displays current culture for main thread and throws InvalidOperationException for created threads.
            Console.WriteLine($"Current UI Culture : {Thread.CurrentThread.CurrentUICulture}");  //// Displays current culture for main thread and throws InvalidOperationException for created threads.
            Thread.Sleep(5000);
        }
    }
}
