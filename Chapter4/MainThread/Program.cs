using System;
using System.Threading;

namespace MainThread
{
    /// <summary>
    /// The Program demonstrates the default Main thread and its properties. 
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            WriteToConsole();
            Console.ReadLine();
        }

        static void WriteToConsole()
        {
            string name = string.IsNullOrWhiteSpace(Thread.CurrentThread.Name) ? "Main Thread" : Thread.CurrentThread.Name;
            Console.WriteLine($"Hello Threading World! from {Thread.CurrentThread.Name ?? "Main Thread"}");  //// Thread name from which message is printed.
            Console.WriteLine($"Managed Thread Id : {Thread.CurrentThread.ManagedThreadId}"); //// The managed thread identifier.
            Console.WriteLine($"IsAlive :{Thread.CurrentThread.IsAlive}"); //// Display if thread is alive.
            Console.WriteLine($"Priority :{Thread.CurrentThread.Priority}");  //// Displays the thread priority. Default is Normal.
            Console.WriteLine($"IsBackground :{Thread.CurrentThread.IsBackground}");  //// Displays if thread is Foreground or Background. By default all threads are foreground.
            Console.WriteLine($"Name :" + name);  //// Name of thread.
            Console.WriteLine($"Apartment State :{Thread.CurrentThread.ApartmentState.ToString()}"); //// This is obsolete and is shown for the sake of completeness. The apartment state.
            Console.WriteLine($"IsThreadPoolThread :{Thread.CurrentThread.IsThreadPoolThread}"); //// Displays if thread is a CLR thread pool thread
            Console.WriteLine($"ThreadState :{Thread.CurrentThread.ThreadState}");  //// Displays the thread state
            Console.WriteLine($"Current Culture : {Thread.CurrentThread.CurrentCulture}"); //// Displays current culture for main thread and throws InvalidOperationException for created threads.
            Console.WriteLine($"Current UI Culture : {Thread.CurrentThread.CurrentUICulture}");  //// Displays current culture for main thread and throws InvalidOperationException for created threads.            
        }
    }
}
