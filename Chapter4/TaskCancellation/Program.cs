using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskCancellation
{
    /// <summary>
    /// Demonstrates the Task Cancellation.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();
            Task t = Task.Run(() => MethodThatCanBeCancelled(1000000, cts.Token), cts.Token);

            try
            {
                cts.CancelAfter(1000);
                t.Wait();
            }
            catch (AggregateException ex)
            {
                ex.Handle((e) =>
                {
                    if (e is OperationCanceledException)
                    {
                        Console.WriteLine($"Operation Cancelled! {e.Message}");
                        return true;
                    }

                    return false;
                });
            }
            finally
            {
                cts.Dispose();
            }

            Console.ReadLine();
        }

        static void MethodThatCanBeCancelled(object state, CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                Console.WriteLine($"Cancellation requested even before it started...");
                token.ThrowIfCancellationRequested();
            }

            int length = (int)state;
            for (int i = 0; i < length; i++)
            {
                ////Simulate the work that runs for a while.
                Task.Delay(5000);

                if (token.IsCancellationRequested)
                {
                    Console.WriteLine($"Cancellation requested while running iteration # {i}");
                    token.ThrowIfCancellationRequested();
                }
            }
        }
    }
}
