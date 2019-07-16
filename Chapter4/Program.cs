using System;
using System.Threading;

namespace DemystifyingThreading
{
    class Program
    {
        static void Main(string[] args)
        {
            Write();

            Thread t = new Thread(Write) { Name = "Thread 1" };
            t.Start();

            ////Thread t2 = new Thread(Write) { Name = "Thread 2" };
            ////t2.Start();

            Console.ReadLine();
        }

        static void Write()
        {
            string name = string.IsNullOrWhiteSpace(Thread.CurrentThread.Name) ? "Main Thread" : Thread.CurrentThread.Name;
            Console.WriteLine($"Hello Threading World! from {Thread.CurrentThread.Name ?? "Main Thread"}");
            Console.WriteLine("IsAlive :" + Thread.CurrentThread.IsAlive);
            Console.WriteLine("Priority :" + Thread.CurrentThread.Priority);
            Console.WriteLine("IsBackground :" + Thread.CurrentThread.IsBackground);
            Console.WriteLine("Name :" + name);
            Console.WriteLine("Apartment State :" + Thread.CurrentThread.ApartmentState.ToString());
            Console.WriteLine("IsThreadPoolThread :" + Thread.CurrentThread.IsThreadPoolThread);
            Console.WriteLine("ThreadState :" + Thread.CurrentThread.ThreadState);
            Thread.Sleep(5000);
        }
    }                                          
}
