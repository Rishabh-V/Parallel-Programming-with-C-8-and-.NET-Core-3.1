using System;
using System.Threading.Tasks;

namespace ExceptionHandlingWithTask
{
    /// <summary>
    /// Demonstrates exception handling with Tasks.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Task<int> task = new Task<int>(MethodThatThrowsException, null);

            try
            {                
                task.Start();
                Console.WriteLine($"The result is {task.Result}"); //// task.Wait() would have the same effect.
            }
            catch (AggregateException ex)
            {
                ex.Handle((e) =>
                {
                    if (e is InvalidOperationException)
                    {
                        Console.WriteLine($"Caught Exception - {e.Message}");
                        return true;
                    }

                    return false;
                });
            }

            Console.ReadLine();
        }

        static int MethodThatThrowsException(object state)
        {
            throw new InvalidOperationException("Throwing Exception for demonstrating Exception handling in Tasks");
        }
    }
}
