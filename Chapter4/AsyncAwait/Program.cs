using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace AsyncAwait
{
    /// <summary>
    /// Demonstrates the async-await usage.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
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

            // Call the method.
            DownloadData(url, path);
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
                     
        private static async Task DownloadDataAsync(string url, string path)
        {
            // Create a new web client object
            using (WebClient client = new WebClient()) //1
            {
                // Add user-agent header to avoid forbidden errors.
                client.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; WOW64)"); //2
                // download data from Url
                byte[] data = await client.DownloadDataTaskAsync(url); //3
                // Write data in file.
                using (var fileStream = File.OpenWrite(path)) //4
                {
                    await fileStream.WriteAsync(data, 0, data.Length); //5
                }
            }
        }



    }
}
