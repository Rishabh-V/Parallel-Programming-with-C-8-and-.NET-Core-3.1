using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CPUBoundvsIOBound
{
    class Program
    {
        static async Task Main()
        {
            Console.WriteLine("Before I/O bound task");
            Console.WriteLine("===================================");
            AvailableThreads();
            await Task.Run(() => GetStocks());
            Console.WriteLine("After I/O bound task");
            Console.WriteLine("===================================");            
            AvailableThreads();            
            await Task.Run(() => DoExpensiveCalculation());
            Console.WriteLine("After CPU bound task");
            Console.WriteLine("===================================");
            AvailableThreads();
            Console.ReadLine();
        }

        static void AvailableThreads()
        {
            int worker, io;
            ThreadPool.GetAvailableThreads(out worker, out io);

            Console.WriteLine("Thread pool threads available at startup: ");
            Console.WriteLine("   Worker threads: {0:N0}", worker);
            Console.WriteLine("   Asynchronous I/O threads: {0:N0}", io);
        }

        static async Task GetStocks()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync("https://localhost:44394/api/Stocks");
                    response.EnsureSuccessStatusCode();
                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Data retrieved from API");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"exception occured in API - {ex.Message}");
                }
            }
        }

        static async Task<double> DoExpensiveCalculation()
        {
            Console.WriteLine("Start CPU Bound asynchronous task");
            float calculation = 0;
            var output = await Task.Run(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    calculation = calculation * 20;
                }
                return calculation;
            });
            Console.WriteLine("Finished CPU bound Task");
            return output;
        }
    }
}

