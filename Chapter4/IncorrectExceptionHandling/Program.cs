using System;
using System.Threading;

namespace IncorrectExceptionHandling
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //// Main Thread
                WriteToConsole();

                //// Thread 1
                Thread thread1 = new Thread(WriteToConsole) { Name = "Thread1" };
                thread1.Start();

                //// Thread 2
                Thread thread2 = new Thread(WriteToConsole) { Name = "Thread2" };
                thread2.Start();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception occurred while creating threads {ex.Message}");
            }

            Console.ReadLine();
        }

        static void WriteToConsole()
        {
            string name = string.IsNullOrWhiteSpace(Thread.CurrentThread.Name) ? "Main Thread" : Thread.CurrentThread.Name;
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
            if (!string.Equals(name, "Main Thread"))
            {
                throw new InvalidOperationException();
            }
        }
    }
}
