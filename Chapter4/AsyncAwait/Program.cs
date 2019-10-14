using System;
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
            // Prevent the program from exiting unless you press enter.
            Console.ReadLine();
        }

        private static async Task DownloadDataAsync(string url, string path)
        {
            // Create a new web client object
            using (WebClient client = new WebClient()) //1
            {
                // Add user-agent header to avoid forbidden errors.
                client.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; WOW64)"); //2
                // download data from Url
                // Uncomment below code to see that this is running in Main thread
                //await DoNothingAsync();
                //Console.WriteLine("2 - ThreadId " + Thread.CurrentThread.ManagedThreadId);
                byte[] data = await client.DownloadDataTaskAsync(url); //3
                //Console.WriteLine("2 - ThreadId " + Thread.CurrentThread.ManagedThreadId);
                // Write data in file.
                using (var fileStream = File.OpenWrite(path)) //4
                {
                    //Console.WriteLine("3 - ThreadId " + Thread.CurrentThread.ManagedThreadId);
                    await fileStream.WriteAsync(data, 0, data.Length); //5
                    //Console.WriteLine("3 - ThreadId " + Thread.CurrentThread.ManagedThreadId);
                }
            }
        }

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
