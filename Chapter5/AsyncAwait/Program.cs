using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait
{
    /// <summary>
    /// Demonstrates the async-await usage.
    /// </summary>
    class Program
    {
        static async Task Main(string[] args)
        {               
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            // Set the url to a website from which content needs to be downloaded.
            string url = "https://bpbonline.com/collections/c-sharp";
            // Path where downloaded data needs to be saved.
            string path = "C:\\Rishabh\\download.txt";

            // Ensure that the directory of the file path exists.
            var directory = System.IO.Path.GetDirectoryName(path);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Call the asynchronous method.
            //Console.WriteLine("1 - ThreadId " + Thread.CurrentThread.ManagedThreadId);
            await DownloadDataAsync(url, path);
            //Console.WriteLine("1 - ThreadId " + Thread.CurrentThread.ManagedThreadId);
            
            // Async Streams demo
            await PrintAuthorNamesAsync();

            // ValueTask Demo
            await DownloadByteDataAsync(url); 
            
            // Prevent the program from exiting unless you press enter.
            Console.ReadLine();
        }

        private static async Task DownloadDataAsync(string url, string path)
        {
            // Create a new web client object
            using WebClient client = new WebClient();  // C# 8 feature.
            // Add user-agent header to avoid forbidden errors.
            client.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; WOW64)"); //2
            // download data from Url
            // Uncomment below code to see that this is running in Main thread
            //await DoNothingAsync();
            //Console.WriteLine("2 - ThreadId " + Thread.CurrentThread.ManagedThreadId);
            byte[] data = await client.DownloadDataTaskAsync(url); //3
            //Console.WriteLine("2 - ThreadId " + Thread.CurrentThread.ManagedThreadId);
            // Write data in file.
            using var fileStream = File.OpenWrite(path);
            //Console.WriteLine("3 - ThreadId " + Thread.CurrentThread.ManagedThreadId);
            await fileStream.WriteAsync(data, 0, data.Length); //5
        }
            
        private static async Task PrintAuthorNamesAsync()
        {
            // await foreach - Async stream makes it possible.
            await foreach (var item in GetAuthorNamesAsync())
            {
                Console.WriteLine(item);
            }
        }

        private static async IAsyncEnumerable<string> GetAuthorNamesAsync()
        {
            // This dictionary is just to give a demo. 
            // In normal scenarios, we would just have ids for which names would be fetched from database.
            Dictionary<int, string> authorIdNameMappings = new Dictionary<int, string>() { { 1, "Rishabh" }, { 2, "Neha" }, { 3, "Ravindra" } };

            foreach (var id in authorIdNameMappings.Keys)
            {
                //// Simulate Getting name from Web API or network by inserting delay.
                await Task.Delay(300);
                //yield return the detched data
                yield return authorIdNameMappings[id];
            }
        }
                              
        // ValueTask Demo.
        private static async ValueTask<byte[]> DownloadByteDataAsync(string url)
        {   
            if(string.IsNullOrWhiteSpace(url))
            {
                return default;
            }

            // Create a new web client object
            using WebClient client = new WebClient();  // C# 8 feature.
            // Add user-agent header to avoid forbidden errors.
            client.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; WOW64)");
            // download data from Url           
            return await client.DownloadDataTaskAsync(url);
        }

        #region Understanding under the hood 
        //private static Task DownloadDataAsync(string url, string path)
        //{
        //    // Create a new web client object
        //    using (WebClient client = new WebClient())
        //    {
        //        client.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; WOW64)");
        //        // download data from Url
        //        var task = client.DownloadDataTaskAsync(url);
        //        return task.ContinueWith((t) =>
        //        {
        //            byte[] data = task.Result;
        //            using (var fileStream = File.OpenWrite(path))
        //            {
        //                var task2 = fileStream.WriteAsync(data, 0, data.Length);
        //                return task2.ContinueWith((q) =>
        //                {
        //                    // Any other code to continue. Doesn't exist in this case.
        //                    return;
        //                });
        //            }
        //        });
        //    }
        //}


        #endregion

        private static async Task DoNothingAsync()
        {
            Console.WriteLine("4 - ThreadId " + Thread.CurrentThread.ManagedThreadId); // 1 is main thread 
            await Task.Delay(2000);
            Console.WriteLine("4 - ThreadId " + Thread.CurrentThread.ManagedThreadId);
        }

        private static void DownloadData(string url, string path)
        {
            // Create a new web client object
            using (WebClient client = new WebClient())   //1
            {
                // Add user-agent header to avoid forbidden errors.
                client.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; WOW64)"); //2
                // download data from Url
                byte[] data = client.DownloadData(url);  //3
                // Write data in file.
                using (var fileStream = File.OpenWrite(path))  //4
                {
                    fileStream.Write(data, 0, data.Length); //5
                }
            }
        }
    }
}
