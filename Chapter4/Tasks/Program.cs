using System;
using System.Threading;
using System.Threading.Tasks;

namespace Tasks
{
    /// <summary>
    /// Demonstrates creating and using the Tasks.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Task Sample 1");
            //// Task Sample 1  - Create a task only. Don't start
            Task task1 = new Task(WriteToConsole, "ThreadPoolThread");

            Console.WriteLine("Task Sample 2");
            //// Task Sample 2  - Create a started task and wait for it to complete.
            Task task2 = Task.Run(() => WriteToConsole("ThreadPoolThread"));
            task2.Wait();

            //// Start task 1 and wait for it to complete.
            task1.Start();
            task1.Wait();

            Console.WriteLine("Task Sample 3");
            //// Task Sample 3 - Create a started task and wait for it to complete.
            Task task3 = Task.Factory.StartNew(() => WriteToConsole("ThreadPoolThread"));
            task3.Wait();

            Console.WriteLine("Task Sample 4");
            //// Task Sample 4 - Create a task and run it synchronously on main thread and wait for it to completely.
            Task task4 = new Task(WriteToConsole, "ThreadPoolThread");
            task4.RunSynchronously();
            task4.Wait();
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
            Console.WriteLine("--------------------------------------------------------------------------");
            Thread.Sleep(5000);
        }
    }
}
