using System;
using System.Diagnostics;

namespace DebuggingPrimer
{
    class Program
    {
        static void Main(string[] args)
        {
            // Output window
            Debug.WriteLine($"Hello world via Debug statement!");
            Trace.WriteLine($"Hello world via Trace statement!");
            Console.WriteLine("Hello World!");

            // Immediate window
            int aNumber = 100 / 5;
            float anotherNumber = 200 / 66;                    
            // See intellisense and values in immediate window.
                                                               
            Console.ReadLine();
        }
    }
}
