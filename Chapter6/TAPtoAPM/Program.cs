using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace TAPtoAPM
{
    class Program
    {
        static CancellationTokenSource cts = new CancellationTokenSource();
        static void Main(string[] args)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            IAsyncResult result = BeginAPI(null, null);            
            while (!result.IsCompleted) // Proceeding with doing some other operation while file is being read
            {
                if (watch.ElapsedMilliseconds % 3000 == 0)
                {
                    cts.CancelAfter(15000);
                    Console.WriteLine($"Do something else in main method while receiving response from API, Elapsed time - {watch.ElapsedMilliseconds}");
                }
            }
            string apiResponse = EndAPI(result);
            Console.WriteLine($"API response - {apiResponse}, Elapsed time - {watch.ElapsedMilliseconds}");

            //IAsyncResult result = BeginAPI(EndAPIUsingCallback, null); // Using callback            
            Console.ReadKey();
            watch.Stop();
        }

        /// <summary>
        /// Async method to retrieve data from API
        /// </summary>
        /// <returns></returns>
        static async Task<string> GetStocksAsync(CancellationToken token)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync("https://localhost:44394/api/Stocks", token);
                    response.EnsureSuccessStatusCode();
                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Data retrieved from API");
                    return content;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"exception occured in API - {ex.Message}");
                    throw;
                }
            }
        }

        /// <summary>
        /// Begin operation
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        static IAsyncResult BeginAPI(AsyncCallback callback, object state)
        {
            return GetStocksAsync(cts.Token).TAPToApm(callback, state);
        }

        /// <summary>
        /// End operation
        /// </summary>
        /// <param name="asyncResult"></param>
        /// <returns></returns>
        static string EndAPI(IAsyncResult asyncResult)
        {
            try
            {
                return ((Task<string>)asyncResult).Result;
            }
            catch (AggregateException ex)
            {
                List<Tuple<string, string>> errors = ex.Flatten().InnerExceptions.Select(x => new Tuple<string, string>(x.Message, x.StackTrace)).ToList();
                int counter = 0;
                foreach (Tuple<string, string> error in errors)
                {
                    counter++;
                    Console.WriteLine($"{counter}).Error - {error.Item1} \n Innerstack \n {error.Item2} \n");
                }
                return $"Exception occured - {ex.Message}";
            }
        }

        /// <summary>
        /// Callback
        /// </summary>
        /// <param name="asyncResult"></param>
        static void EndAPIUsingCallback(IAsyncResult asyncResult)
        {
            string apiResponse = ((Task<string>)asyncResult).Result;
            Console.WriteLine($"API response - {apiResponse}");
            //return ((Task<string>)asyncResult).Result;
        }
    }

    public static class TaskAPMExtension
    {
        /// <summary>
        /// Generic extension method to convert TAP methods to APM
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="task"></param>
        /// <param name="asyncCallback"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public static IAsyncResult TAPToApm<TResult>(this Task<TResult> task, AsyncCallback asyncCallback, object state)
        {
            var taskCompletionSource = new TaskCompletionSource<TResult>(state);

            task.ContinueWith(delegate
            {
                if (task.IsFaulted)
                {
                    taskCompletionSource.TrySetException(task.Exception.InnerExceptions);
                }
                else if (task.IsCanceled)
                {
                    taskCompletionSource.TrySetCanceled();
                }
                else
                {
                    taskCompletionSource.TrySetResult(task.Result);
                }

                if (asyncCallback != null)
                {
                    asyncCallback(taskCompletionSource.Task);
                }

            }, CancellationToken.None, TaskContinuationOptions.None);

            return taskCompletionSource.Task;
        }
    }
}
